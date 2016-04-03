using ChessClient.Interfaces;
using ChessClient.Interfaces.IControllers;
using ClientAPI;
using GameTemplate.ChessEnums;
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
        public void StartGame(IReadOnlyField figures, FColor color)
        {
            gameScreen.StartGame(figures, color);
        }
        public void Step(ChessState state)
        {
            gameScreen.UpdateField(state);
        }
        public void GameOver(FColor win)
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

            this.screen = gameScreen;

            gameScreen.Send += Send;
            gameScreen.Step += Step;
            gameScreen.Concede += Concede;
        }

        private void Send(ChatMessage msg)
        {
            facade.SendMessage(msg);
        }
        private void Step(StepInfo step)
        {
            facade.MakeStep(step);
        }
        private void Concede()
        {
            facade.Disconnect();
        }


        public void GameClosed(string msg)
        {
            gameScreen.GameClosed(msg);
        }

        public string Nick
        {
            get
            {
                return gameScreen.Nick;
            }
            set
            {
                gameScreen.Nick = value;
            }
        }
    }
}
