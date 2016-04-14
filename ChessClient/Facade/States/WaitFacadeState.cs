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
            Logger.Instance.Log(LogLevel.Warning, "Непредвиденный вызов LoginResult() из WaitFacadeState");
        }
        public void Message(ChatMessage msg)
        {
            Logger.Instance.Log(LogLevel.Warning, "Непредвиденный вызов Message() из WaitFacadeState");
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
            Logger.Instance.Log(LogLevel.Warning, "Непредвиденный вызов UpdateField() из WaitFacadeState");
        }
        public void GameOver(FColor win)
        {
            Logger.Instance.Log(LogLevel.Warning, "Непредвиденный вызов GameOver() из WaitFacadeState");
        }
        public void Waiting()
        {
            Logger.Instance.Log(LogLevel.Warning, "Непредвиденный вызов Waiting() из WaitFacadeState");
        }


        public void Enable()
        {
            manager.Switch(Enums.ScreenType.Wait);
        }


        public void GameClosed(string msg)
        {
            Logger.Instance.Log(LogLevel.Warning, "Непредвиденный вызов GameClosed() из WaitFacadeState");
        }


        public void GetListOnline(string[] online)
        {
           
        }


        public void Disconnect()
        {
            Switch(ClientState.Offline);
        }


        public void StopWait()
        {
            Switch(ClientState.Online);
        }


        public void SetAlgoList(string[] algos)
        {
            Logger.Instance.Log(LogLevel.Warning, "Непредвиденный вызов SetAlgoList() из WaitFacadeState");
        }
    }
}
