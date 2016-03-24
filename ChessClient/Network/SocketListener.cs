using ChessClient.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChessClient.Network
{
    public class SocketListener : ISocketListener
    {
        public SocketListener(IClientFacade clientFacade)
        {           
            this.clientFacade = clientFacade;

            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        private IParser parser;
        private IClientFacade clientFacade;
        private Socket socket;

        private int packetLenght = 1024;

        public void Connect(IPAddress id, int port)
        {
            try
            {
                socket.Connect(new IPEndPoint(id, port));

                IsRunning = true;
                new Task(Listen).Start();
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
                byte[] buffer = new byte[packetLenght];
                while (true)
                {
                    socket.Receive(buffer);

                    parser.Parse(buffer);
                }
            }
            catch
            {
                IsRunning = false;
            }
        }


        public bool IsRunning
        {
            get;
            private set;
        }


        public void SetParser(IParser parser)
        {
            this.parser = parser;
        }
    }
}
