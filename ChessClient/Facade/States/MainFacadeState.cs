using ChessClient.Interfaces;
using GameTemplate.ChessGame.ChessInterfaces;
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
    class MainFacadeState : IFacadeState
    {
        public MainFacadeState(IScreenManager manager)
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
            manager.MainController.Message(msg);
        }
        public void Disconnected()
        {
            Switch(Enums.ClientState.Offline);
        }
        public void StartGame(System.Drawing.Color color)
        {
            manager.GameController.StartGame(color);

            Switch(Enums.ClientState.InGame);
        }
        public void Challenge(string from)
        {
            manager.MainController.Challenge(from);
        }
        public void UpdateField(IReadOnlyField field, StepInfo step)
        {
            throw new NotImplementedException();
        }
        public void GameOver(bool win)
        {
            throw new NotImplementedException();
        }
        public void Waiting()
        {
            Switch(Enums.ClientState.Waiting);
        }


        public void Enable()
        {
            manager.Switch(Enums.ScreenType.Main);
        }
    }
}
