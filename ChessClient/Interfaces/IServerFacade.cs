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
        void LogIn(IPAddress ip);
        void StartRandomGame();
        void StartGameWith(string gamer);
        void MakeStep(StepInfo step);
        void SendMessage(string msg);
        void Surrender();
        void Accept();
    }
}
