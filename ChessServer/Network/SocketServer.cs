using ChessServer.Interfaces;
using Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChessServer
{
    [Serializable]
    class SocketServer : IServer
    {
        public SocketServer(IIDManager idManager, IParser parser)
        {
            this.idManager = idManager;
            this.parser = parser;

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clients = new Dictionary<int, Socket>();
        }

        private IIDManager idManager;
        private Socket socket;
        private Dictionary<int, Socket> clients;
        private IParser parser;
        private object lck = new object();

        private int max = 10;
        private int packetLenght = 5024;
        private int port = 8888;

        public void Start()
        {
            Logger.Instance.Log(LogLevel.Info, "Сервер запущен");
            Accept();
        }
        public void Stop()
        {
           
        }
        public void Send(byte[] msg, int id)
        {
            if (clients.ContainsKey(id))
            {
                clients[id].Send(msg);
            }
        }
        public void Disconnect(int id)
        {
            if (clients.ContainsKey(id))
            {
                clients[id].Dispose();
            }
        }

        private void Accept()
        {
            socket.Bind(new IPEndPoint(IPAddress.Any, port));
            socket.Listen(max);
            int id;

            while (true)
            {
                try
                {
                    var client = socket.Accept();
                    id = idManager.GetId();
                    lock (lck)
                    {
                        clients.Add(id, client);
                    }
                    
                    Connected(id);


                    ThreadPool.QueueUserWorkItem((x) => Listen(id, client));
                }
                catch
                {

                    Logger.Instance.Log(LogLevel.Info, "Сервер остановлен");
                }
            }
        }
        private void Listen(int id, Socket socket)
        {
            Logger.Instance.Log(LogLevel.Info, "Новое подключение, id = " + id.ToString());

            byte[] arr = new byte[packetLenght];
            int len;

            try
            {
                while (true)
                {
                    len = socket.Receive(arr);

                    parser.Parse(arr.Take(len).ToArray(), id);
                }
            }
            catch
            {
                Logger.Instance.Log(LogLevel.Info, "Сокет с id = " + id.ToString() + " отключился");

                Disconnected(id);

                lock (lck)
                {
                    clients.Remove(id);
                }

                idManager.Delete(id);
            }

        }

        public event Action<int> Connected = (x) => { };
        public event Action<int> Disconnected = (x) => { };

    }
}
