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
using System.Windows.Forms;

namespace ClientAPI
{
    public interface IGameScreen : IMessenger, IScreen
    {
        event Action<StepInfo> Step;
        event Action Concede;

        void StartGame(IReadOnlyField figures, FColor color);
        void UpdateField(ChessState state);
        void SetRender(IRender r);
        void GameOver(FColor win);
        void GameClosed(string msg);
    }

    //public abstract class AbstractGameScreen : UserControl, IMessenger
    //{
    //    public event Action<StepInfo> Step;

    //    public abstract void StartGame();
    //    public abstract void UpdateField(IField<IChessFigure> f);
    //    public abstract void SetRender(IRender r);
    //    public abstract void Message(string msg);
    //    public abstract void GameOver(bool win);

    //    public abstract event Action<string> Send;

    //    public abstract void Receive(string message);
    //}

}
