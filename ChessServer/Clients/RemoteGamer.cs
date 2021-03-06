﻿using ChessServer.Interfaces;
using GameTemplate.ChessEnums;
using GameTemplate.Game;
using GameTemplate.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChessServer.Clients
{
    class RemoteGamer :IGamer
    {
        public RemoteGamer()
        {
            wait = new EventWaitHandle(false, EventResetMode.AutoReset);
        }

        private IGame game;
        private FColor color;
        private StepInfo step;
        private EventWaitHandle wait;
        private bool isWait;
        private bool inGame;

        public void Step(StepInfo step)
        {
            if (inGame && isWait)
            {
                this.step = step;
                isWait = false;

                wait.Set();
            }
        }

        public FColor Color
        {
            get { return color; }
        }

        public void Init(IGame game, FColor color)
        {
            this.game = game;
            this.color = color;
            this.inGame = true;
            this.isWait = false;

        }

        public GameTemplate.Game.StepInfo MakeStep()
        {
            isWait = true;

            wait.WaitOne();

            return step;
        }


        public void GameOver()
        {
            inGame = false;
            if (isWait)
            {
                wait.Set();
            }

            isWait = false;

            
        }
    }
}
