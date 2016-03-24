using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ChessClient.Interfaces
{
    public interface ISocketListener
    {
        void Connect(IPAddress id, int port);
        void Stop();

        void Send(byte[] arr);
    }
}
