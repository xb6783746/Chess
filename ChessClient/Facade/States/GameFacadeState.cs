using ChessClient.Enums;
using ChessClient.Interfaces;
using GameTemplate.Game;
using GameTemplate.Interfaces;
using Network;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClient.Facade.States
{
    class GameFacadeState : IFacadeState
    {
        public GameFacadeState(IScreenManager manager)
        {
            this.manager = manager;  
        }

        private IScreenManager manager;

        public event Action<ClientState> Switch;

        public void LoginResult(bool result, string message)
        {
            throw new NotImplementedException();
        }
        public void Message(ChatMessage msg)
        {
            manager.GameController.Message(msg);
        }
        public void Disconnected()
        {
            Switch(ClientState.Offline);
        }
        public void StartGame(Color color)
        {
            manager.GameController.StartGame(color);
            Switch(ClientState.InGame);
        }
        public void Challenge(string from)
        {
            //manager.MainController.Challenge(from);
        }
        public void UpdateField(IReadOnlyList<FigureOnBoard> field, StepInfo step)
        {
            manager.GameController.Step(field, step);
        }
        public void GameOver(bool win)
        {
            manager.GameController.GameOver(win);
        }
        public void Waiting()
        {
            //Switch(ClientState.Waiting);
        }


        public void Enable()
        {
            manager.Switch(ScreenType.Game);
        }
    }
}
