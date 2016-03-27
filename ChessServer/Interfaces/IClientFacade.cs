using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessServer.Interfaces
{
    interface IClientFacade
    {
        void Message(string msg, int id);
        void LoginResult(bool result, string message, int id);
    }
}
