using GameTemplate.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ChessClient.Interfaces
{
    public interface IServerFacade
    {
        void LogIn(IPAddress ip, int port, string nick);

        void GetAllGamers();

        void RandomGame();
        void GameWith(string nick);
        void WatchFor(string nick);

        void MakeStep(StepInfo step);

        void SendMessage(string message);
    }
}
