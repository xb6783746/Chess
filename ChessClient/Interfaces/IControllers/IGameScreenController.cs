using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClient.Interfaces.IControllers
{
    interface IGameScreenController
    {
        void Message(string msg);

        void StartGame();

        void Step(IChessField f, StepInfo step);

        void GameOver(bool win);
    }
}
