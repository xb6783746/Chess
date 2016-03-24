using ChessClient.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ChessClient.Network
{
    class SocketListener : ISocketListener
    {
        public void Connect(IPAddress id, int port)
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void Send(byte[] arr)
        {
            throw new NotImplementedException();
        }
    }
}
