﻿using GameTemplate.ChessEnums;
using GameTemplate.Game;
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
        string Nick { get; set; }

        void Message(ChatMessage msg);
        void StartGame(IReadOnlyField figures, FColor color);
        void Step(ChessState state);
        void GameOver(FColor win);
        void GameClosed(string msg);
    }
}
