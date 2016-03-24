using ChessClient.Interfaces;
using ChessClient.Interfaces.IControllers;
using ClientAPI;
using GameTemplate.ChessGame.ChessInterfaces;
using GameTemplate.Game;
using GameTemplate.Interfaces;
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

        public void Message(string msg)
        {
            gameScreen.Message(msg);
        }
        public void StartGame(Color color)
        {
            gameScreen.StartGame();
        }
        public void Step(IField<IChessFigure> f, StepInfo step)
        {
            gameScreen.UpdateField(f);
        }
        public void GameOver(bool win)
        {
            gameScreen.GameOver(win);
        }

        protected override void LoadScreen()
        {
            gameScreen = null;

            gameScreen.Send += Send;
            gameScreen.Step += Step;
        }

        private void Send(string msg)
        {
            facade.SendMessage(msg);
        }
        private void Step(StepInfo step)
        {
            facade.MakeStep(step);
        }
    }
}
