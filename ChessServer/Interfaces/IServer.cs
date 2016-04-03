using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessServer.Interfaces
{
    interface IServer
    {
        void Start();
        void Stop();

        void Send(byte[] msg, int id);
        void Disconnect(int id);

        event Action<int> Connected;
        event Action<int> Disconnected;
        event Action<byte[], int> Receive;
    }
}
