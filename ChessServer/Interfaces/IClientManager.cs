using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessServer.Interfaces
{
    interface IClientManager
    {
        int Registration();
        void Disconnect(int id);
    }
}
