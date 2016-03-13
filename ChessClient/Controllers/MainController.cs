using ChessClient.Enums;
using ChessClient.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClient.Controllers
{
    class MainController :IClientFacade
    {
        private ClientState State { get; private set; }

        public void Connect(bool isConnected, string nick)
        {
            throw new NotImplementedException();
        }
        public void SetGamerList(List<string> gamers)
        {
            throw new NotImplementedException();
        }

        public void StartGame()
        {
            throw new NotImplementedException();
        }
        public void Update(GameTemplate.Game.StepInfo step, bool turn)
        {
            throw new NotImplementedException();
        }
        public void GameOver(bool win)
        {
            throw new NotImplementedException();
        }
        public void Surrendered()
        {
            throw new NotImplementedException();
        }
        public void StartWatch()
        {
            throw new NotImplementedException();
        }

        public void NewMessage(string message)
        {
            throw new NotImplementedException();
        }
    }
}
