﻿using ChessClient.Enums;
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
        public void GameOver(FColor win)
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


        public void GameClosed(string msg)
        {
            throw new NotImplementedException();
        }


        public void GetListOnline(string[] online)
        {
            manager.MainController.SetOnlineList(online);
        }


        public void Disconnect()
        {
            Switch(ClientState.Offline);
        }
    }
}
