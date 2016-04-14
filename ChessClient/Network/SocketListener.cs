﻿using ChessClient.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChessClient.Network
{
    public class SocketListener : ISocketListener, IDisposable
    {
        public SocketListener(IClientFacade clientFacade)
        {
            this.clientFacade = clientFacade;

            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.messages = new Queue<byte[]>();
            this.wait = new AutoResetEvent(false);

            disposable = new List<IDisposable>()
            {
                wait, socket
            };
        }

        private IParser parser;
        private Queue<byte[]> messages;
        private AutoResetEvent wait;
        private IClientFacade clientFacade;
        private Socket socket;
        private object lck = new object();

        private List<IDisposable> disposable;

        private int packetLenght = 5000;

        public void SetParser(IParser parser)
        {
            this.parser = parser;
        }
        public bool IsRunning
        {
            get;
            private set;
        }
        public void Connect(IPAddress id, int port)
        {
            try
            {
                socket.Connect(new IPEndPoint(id, port));

                IsRunning = true;
                Task.Run(() => Execute());
                Task.Run(() => Listen());
            }
            catch
            {
                clientFacade.LoginResult(false, "Установить соединение не удалось");
            }
        }
        public void Stop()
        {
            socket.Close();
        }
        public void Send(byte[] arr)
        {
            socket.Send(arr);
        }

        private void Listen()
        {
            try
            {
                int len;
                byte[] buffer = new byte[packetLenght];
                while (true)
                {
                    len = socket.Receive(buffer);

                    var tmp = buffer.Take(len).ToArray();


                    lock (lck)
                    {
                        messages.Enqueue(tmp);
                    }

                    wait.Set();
                }
            }
            catch
            {
                IsRunning = false;
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientFacade.Disconnect();
            }
        }
        private void Execute()
        {
            byte[] mess;

            while (IsRunning)
            {

                while (messages.Count > 0)
                {
                    lock (lck)
                    {
                        mess = messages.Dequeue();
                    }

                    parser.Parse(mess);
                }

                wait.WaitOne();
            }
        }

        public void Dispose()
        {
            foreach (var item in disposable)
            {
                item.Dispose();
            }
        }
    }
}
