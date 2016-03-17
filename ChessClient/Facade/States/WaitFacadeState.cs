using ChessClient.Interfaces;
using GameTemplate.ChessGame.ChessInterfaces;
using GameTemplate.Game;
using GameTemplate.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClient.Facade.States
{
    class WaitFacadeState : IFacadeState
    {
        public WaitFacadeState(IScreenManager manager)
        {
            this.manager = manager;  
        }

        private IScreenManager manager;
        
        public event Action<Enums.ClientState> Switch;
        public void LoginResult(bool result, string message)
        {
            throw new NotImplementedException();
        }
        public void Message(string msg)
        {
            throw new NotImplementedException();
        }
        public void Disconnected()
        {
            throw new NotImplementedException();
        }
        public void StartGame(System.Drawing.Color color)
        {
            throw new NotImplementedException();
        }
        public void Challenge(string from)
        {
            throw new NotImplementedException();
        }
        public void UpdateField(IField<IChessFigure> field, StepInfo step)
        {
            throw new NotImplementedException();
        }
        public void GameOver(bool win)
        {
            throw new NotImplementedException();
        }
        public void Waiting()
        {
            throw new NotImplementedException();
        }
    }
}
