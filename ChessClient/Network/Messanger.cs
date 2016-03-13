using ChessClient.Interfaces;
using GameTemplate.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ChessClient.Network
{
    class Messanger :IServerFacade
    {
        public void LogIn(IPAddress ip, int port, string nick)
        {
            throw new NotImplementedException();
        }

        public void GetAllGamers()
        {
            throw new NotImplementedException();
        }

        public void RandomGame()
        {
            throw new NotImplementedException();
        }
        public void GameWith(string nick)
        {
            throw new NotImplementedException();
        }
        public void WatchFor(string nick)
        {
            throw new NotImplementedException();
        }

        public void MakeStep(StepInfo step)
        {
            throw new NotImplementedException();
        }

        public void SendMessage(string message)
        {
            throw new NotImplementedException();
        }
    }
}
