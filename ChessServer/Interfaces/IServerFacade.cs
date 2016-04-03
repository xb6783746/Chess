using GameTemplate.Game;
using Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessServer.Interfaces
{
    interface IServerFacade
    {
        void Message(ChatMessage message, int id);
        void ChangeNick(string nick, int id);
        void NewStep(StepInfo step, int id);


        void RandomGame(int id);
    }
}
