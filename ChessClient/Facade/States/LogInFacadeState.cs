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
using GameTemplate.ChessEnums;
using Log;

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
                manager.GameController.Nick = message;
                manager.MainController.SetNick(message);
                Switch(ClientState.Online);
            }
            else
            {
                manager.LoginController.Fail(message);
            }
        }
        public void Message(ChatMessage msg)
        {
            Logger.Instance.Log(LogLevel.Warning, "Непредвиденный вызов Message() из LogInFacadeState");
        }
        public void Disconnected()
        {
            Switch(ClientState.Offline);
        }
        public void StartGame(IReadOnlyField figures, FColor color)
        {
            Logger.Instance.Log(LogLevel.Warning, "Непредвиденный вызов StartGame() из LogInFacadeState");
        }
        public void Challenge(string from)
        {
            Logger.Instance.Log(LogLevel.Warning, "Непредвиденный вызов Challenge() из LogInFacadeState");
        }
        public void UpdateField(ChessState state)
        {
            Logger.Instance.Log(LogLevel.Warning, "Непредвиденный вызов UpdateField() из LogInFacadeState");
        }
        public void GameOver(FColor win)
        {
            Logger.Instance.Log(LogLevel.Warning, "Непредвиденный вызов GameOver() из LogInFacadeState");
        }
        public void Waiting()
        {
            Logger.Instance.Log(LogLevel.Warning, "Непредвиденный вызов Waiting() из LogInFacadeState");
        }


        public void Enable()
        {
            manager.Switch(Enums.ScreenType.LogIn);
        }


        public void GameClosed(string msg)
        {
            Logger.Instance.Log(LogLevel.Warning, "Непредвиденный вызов GameClosed() из LogInFacadeState");
        }


        public void GetListOnline(string[] online)
        {
            Logger.Instance.Log(LogLevel.Warning, "Непредвиденный вызов GetListOnline() из LogInFacadeState");
        }


        public void Disconnect()
        {
            Switch(ClientState.Offline);
        }


        public void StopWait()
        {
            Logger.Instance.Log(LogLevel.Warning, "Непредвиденный вызов StopWait() из LogInFacadeState");
        }


        public void SetAlgoList(string[] algos)
        {
            Logger.Instance.Log(LogLevel.Warning, "Непредвиденный вызов SetAlgoList() из LogInFacadeState");
        }
    }
}
