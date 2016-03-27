using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessServer.Interfaces
{
    interface IServerFacade
    {
        void Message(string message, int id);
    }
}
