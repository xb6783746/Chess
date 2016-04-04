using Network;
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
        void Message(ChatMessage msg);
        void SetNick(string nick);
        void SetOnlineList(string[] online);
    }
}
