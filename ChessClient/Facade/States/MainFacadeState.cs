using ChessClient.Enums;
using ChessClient.Interfaces;
using GameTemplate.ChessEnums;
using GameTemplate.Game;
using GameTemplate.Interfaces;
using Log;
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
            Logger.Instance.Log(LogLevel.Warning, "Непредвиденный вызов LoginResult() из MainFacadeState");
        }
        public void Message(ChatMessage msg)
        {
            manager.MainController.Message(msg);
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
            Logger.Instance.Log(LogLevel.Warning, "Непредвиденный вызов UpdateField() из MainFacadeState");
        }
        public void GameOver(FColor win)
        {
            Logger.Instance.Log(LogLevel.Warning, "Непредвиденный вызов GameOver() из MainFacadeState");
        }
        public void Waiting()
        {
            Switch(Enums.ClientState.Waiting);
        }


        public void Enable()
        {
            manager.Switch(Enums.ScreenType.Main);
        }


        public void GameClosed(string msg)
        {
            Logger.Instance.Log(LogLevel.Warning, "Непредвиденный вызов GameClosed() из MainFacadeState");
        }


        public void GetListOnline(string[] online)
        {
            manager.MainController.SetOnlineList(online);
        }


        public void Disconnect()
        {
            Switch(ClientState.Offline);
        }


        public void StopWait()
        {
            Logger.Instance.Log(LogLevel.Warning, "Непредвиденный вызов StopWait() из MainFacadeState");
        }


        public void SetAlgoList(string[] algos)
        {
            manager.MainController.SetAlgoList(algos);
        }
    }
}
