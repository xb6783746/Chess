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
        void StartGame(IReadOnlyList<FigureOnBoard> figures, Color color);
        void Step(ChessState state);
        void GameOver(bool win);
    }
}
