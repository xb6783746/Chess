﻿using ChessClient.Interfaces;
using ChessClient.Interfaces.IControllers;
using ClientAPI;
using GameTemplate.Game;
using GameTemplate.Interfaces;
using Network;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClient.Controllers
{
    class GameScreenController : BasicController, ISwitch, IGameScreenController
    {

        public GameScreenController(IMainForm form, IServerFacade facade):base(form, facade)
        {              
        }

        private IGameScreen gameScreen;

        public void Message(ChatMessage msg)
        {
            gameScreen.Receive(msg);
        }
        public void StartGame(Color color)
        {
            gameScreen.StartGame(color);
        }
        public void Step(IReadOnlyField f, StepInfo step)
        {
            gameScreen.UpdateField(f);
        }
        public void GameOver(bool win)
        {
            gameScreen.GameOver(win);
        }

        protected override void LoadScreen()
        {
            var gScreen = this.GetScreenType("/Screens", typeof(IGameScreen));

            var renderType = this.GetScreenType("/Screens", typeof(IRender));

            var render = Activator.CreateInstance(renderType) as IRender;

            gameScreen = Activator.CreateInstance(gScreen) as IGameScreen;
            gameScreen.SetRender(render);

            this.screen = gameScreen.GetScreen();

            gameScreen.Send += Send;
            gameScreen.Step += Step;
        }

        private void Send(ChatMessage msg)
        {
            facade.SendMessage(msg);
        }
        private void Step(StepInfo step)
        {
            facade.MakeStep(step);
        }
    }
}
