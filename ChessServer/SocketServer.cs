using ChessServer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChessServer
{
    class SocketServer :IServer
    {
        struct Client
        {
            public Client(Socket socket, int id)
            {
                this.socket = socket;
                this.id = id;
            }

            private int id;
            private Socket socket;

            public int ID
            {
                get { return id; }
            }
            public Socket Socket
            {
                get { return socket; }
            }
        }
        public SocketServer(IParser parser, IClientManager clientManager)
        {
            this.parser = parser;
            this.clientManager = clientManager;

            socket = new Socket(SocketType.Stream, ProtocolType.IPv4);
            clients = new List<Client>();
        }

        private IParser parser;
        private IClientManager clientManager;
        private Socket socket;

        private List<Client> clients;

        private object lck = new object();

        private int max = 10;
        private int packetLenght = 1024;

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
            var client = clients.FirstOrDefault((x) => x.ID == id);

            if (client.Socket != null)
            {
                client.Socket.Send(msg);
            }
        }


        private void Accept()
        {
            socket.Listen(max);
            int id;

            while (true)
            {
                try
                {
                    var client = socket.Accept();

                    id = clientManager.Registration();

                    lock (lck)
                    {
                        clients.Add(new Client(client, id));
                    }

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

            while (true)
            {
                try
                {
                    len = socket.Receive(arr);

                    parser.Parse(arr.Take(len).ToArray(), id);
                }
                catch
                {
                    clientManager.Disconnect(id);
                }
            }
        }
    }
}
