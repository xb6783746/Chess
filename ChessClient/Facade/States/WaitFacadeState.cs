using ChessClient.Interfaces;
using GameTemplate.ChessEnums;
using GameTemplate.Game;
using GameTemplate.Interfaces;
using Network;
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
        public void Message(ChatMessage msg)
        {
            throw new NotImplementedException();
        }
        public void Disconnected()
        {
            Switch(Enums.ClientState.Offline);
        }
        public void StartGame(IReadOnlyField figures, FColor color)
        {
            manager.GameController.StartGame(figures, color);

            Switch(Enums.ClientState.InGame);
        }
        public void Challenge(string from)
        {
            manager.MainController.Challenge(from);
        }
        public void UpdateField(ChessState state)
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


        public void Enable()
        {
            manager.Switch(Enums.ScreenType.Wait);
        }
    }
}
