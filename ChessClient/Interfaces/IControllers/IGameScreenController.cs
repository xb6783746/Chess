using GameTemplate.ChessGame.ChessInterfaces;
using GameTemplate.Game;
using GameTemplate.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClient.Interfaces.IControllers
{
    interface IGameScreenController
    {
        void Message(string msg);
        void StartGame(Color color);
        void Step(IField f, StepInfo step);
        void GameOver(bool win);
    }
}
