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
        void GetOnline(int id);
        void GetAlgoList(int id);
        void GameWith(string with, int id);
        void GameWithAnswer(bool ans, int id);
        void RandomGame(int id);
    }
}
