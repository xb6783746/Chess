using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClient.Interfaces.IControllers
{
    interface IMainScreenController
    {
        void Challenge(string from);
        void Message(string msg);
    }
}
