using ChessClient.Interfaces;
using GameTemplate.Game;
using GameTemplate.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessClient.Enums;
using Network;

namespace ChessClient.Facade.States
{
    class LogInFacadeState :IFacadeState
    {
        public LogInFacadeState(IScreenManager manager)
        {
            this.manager = manager;  
        }

        private IScreenManager manager;

        public event Action<ClientState> Switch;

        public void LoginResult(bool result, string message)
        {
            if (result)
            {
                Switch(ClientState.Online);
            }
            else
            {
                manager.LoginController.Fail(message);
            }
        }
        public void Message(ChatMessage msg)
        {
            throw new NotImplementedException();
        }
        public void Disconnected()
        {
            Switch(ClientState.Offline);
        }
        public void StartGame(IReadOnlyList<FigureOnBoard> figures, Color color)
        {
            throw new NotImplementedException();
        }
        public void Challenge(string from)
        {
            throw new NotImplementedException();
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
            manager.Switch(Enums.ScreenType.LogIn);
        }
    }
}
