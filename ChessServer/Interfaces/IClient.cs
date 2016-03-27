using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessServer.Interfaces
{
    interface IClient
    {
        int Id { get; }
        string Nick { get; set; }

        void Send(string mesg);
        void LoginResult(bool result, string message);
    }
}
