﻿using GameTemplate.Game;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClient.Interfaces
{
    public interface IClientFacade
    {
        void LoginResult(bool result, string message);
        void Message(string msg);
        void Disconnected();
        void StartGame(Color color);
        void Challenge(string from);

        void UpdateField(IChessField field, StepInfo step);

        void GameOver(bool win);
        void Waiting();
    }
}
