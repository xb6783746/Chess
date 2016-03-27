using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessServer.Interfaces
{
    interface IClientFacade
    {
        void SendMessage(string msg, int id);
    }
}
