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

        public void LogIn(IPAddress ip, int port, string message)
        {
            throw new NotImplementedException();
        }

        public void StartRandomGame()
        {
            throw new NotImplementedException();
        }
        public void StartGameWith(string gamer)
        {
            throw new NotImplementedException();
        }
        public void MakeStep(StepInfo step)
        {
            throw new NotImplementedException();
        }
        public void SendMessage(string msg)
        {
            throw new NotImplementedException();
        }
        public void Surrender()
        {
            throw new NotImplementedException();
        }
        public void Accept()
        {
            throw new NotImplementedException();
        }
        public void ChangeNick(string nick)
        {
            throw new NotImplementedException();
        }
        public void WatchFor(string gamer)
        {
            throw new NotImplementedException();
        }
    }
}
