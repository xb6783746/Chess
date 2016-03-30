using GameTemplate.Game;
using GameTemplate.Interfaces;
using Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessServer.Interfaces
{
    interface IClientFacade
    {
        void Message(ChatMessage msg, int id);
        void LoginResult(bool result, string message, int id);

        void Update(IReadOnlyField field, StepInfo step, int id);
    }
}
