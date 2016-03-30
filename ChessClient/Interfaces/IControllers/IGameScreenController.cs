﻿using GameTemplate.Game;
using GameTemplate.Interfaces;
using Network;
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
        void Message(ChatMessage msg);
        void StartGame(Color color);
        void Step(IReadOnlyField f, StepInfo step);
        void GameOver(bool win);
    }
}
