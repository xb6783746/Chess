﻿using ChessClient.Interfaces;
using ChessClient.Interfaces.IControllers;
using ClientAPI;
using Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ChessClient.Controllers
{
    class MainScreenController : BasicController, ISwitch, IMainScreenController
    {
        public MainScreenController(IMainForm form, IServerFacade facade) :base(form, facade)
        {

        }

        public void Challenge(string from)
        {
            mainScreen.Challenge(from);
        }
        public void Message(ChatMessage msg)
        {
            mainScreen.Receive(msg);
        }
        public void SetNick(string nick)
        {
            mainScreen.Nick = nick;
        }


        private IMainScreen mainScreen;

        protected override void LoadScreen()
        {
            var screen = GetScreenType("/Screens", typeof(IMainScreen));

            mainScreen = Activator.CreateInstance(screen) as IMainScreen;
            this.screen = mainScreen;

            mainScreen.ChangeNick += ChangeNick;
            mainScreen.GameWith += GameWith;
            mainScreen.RandomGame += RandomGame;
            mainScreen.Send += Send;
            mainScreen.WatchForGamer += Watch;
            mainScreen.GetOnline += GetOnlineList;
        }

        private void ChangeNick(string nick)
        {
            facade.ChangeNick(nick);
        }
        private void GameWith(string gamer)
        {
            facade.StartGameWith(gamer);
        }
        private void RandomGame()
        {
            facade.StartRandomGame();
        }
        private void Send(ChatMessage message)
        {
            facade.SendMessage(message);
        }
        private void Watch(string gamer)
        {
            facade.WatchFor(gamer);
        }
        private void GetOnlineList()
        {
            facade.GetOnline(); 
        }
        public void SetOnlineList(string[] online)
        {
            mainScreen.SetOnlineList(online);   
        }
    }
}
