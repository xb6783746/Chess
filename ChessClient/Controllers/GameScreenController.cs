using ChessClient.Interfaces;
using ChessClient.Interfaces.IControllers;
using GameTemplate.ChessGame.ChessInterfaces;
using GameTemplate.Game;
using GameTemplate.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClient.Controllers
{
    class GameScreenController : ISwitch, IGameScreenController
    {
        public void Enable()
        {
            throw new NotImplementedException();
        }
        public void Disable()
        {
            throw new NotImplementedException();
        }

        public void Message(string msg)
        {
            throw new NotImplementedException();
        }
        public void StartGame()
        {
            throw new NotImplementedException();
        }
        public void Step(IField<IChessFigure> f, StepInfo step)
        {
            throw new NotImplementedException();
        }
        public void GameOver(bool win)
        {
            throw new NotImplementedException();
        }
    }
}
