using ChessServer.Interfaces;
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
        public SocketServer(IClientManager clientManager, IIDManager idManager)
        {
            this.clientManager = clientManager;
            this.idManager = idManager;

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clients = new Dictionary<int, Socket>();
        }

        private IClientManager clientManager;
        private IIDManager idManager;
        private Socket socket;
        private Dictionary<int, Socket> clients;
        private object lck = new object();

        private int max = 10;
        private int packetLenght = 1024;
        private int port = 8888;

        public void Start()
        {
            Accept();
        }
        public void Stop()
        {
            throw new NotImplementedException();
        }
        public void Send(byte[] msg, int id)
        {
            if (clients.ContainsKey(id))
            {
                clients[id].Send(msg);
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

                    clientManager.Registration(id);


                    ThreadPool.QueueUserWorkItem((x) => Listen(id, client));
                }
                catch
                {

                    throw;
                }
            }
        }
        private void Listen(int id, Socket socket)
        {
            byte[] arr = new byte[packetLenght];
            int len;

           // try
           // {
                while (true)
                {
                    len = socket.Receive(arr);

                    Receive(arr.Take(len).ToArray(), id);
                }
           // }
            //catch
           // {
                clientManager.Disconnect(id);

                lock (lck)
                {
                    clients.Remove(id);
                }
                idManager.Delete(id);
           // }

        }

        public event Action<byte[], int> Receive = (x, y) => { };
    }
}
