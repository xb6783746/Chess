using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessServer.Interfaces
{
    interface IClientManager
    {
        void Registration(int id);
        void Disconnect(int id);

        void ChangeNick(string nick, int id);

        event Action<IClient> Connected;
        event Action<IClient> Disconnected;
    }
}
