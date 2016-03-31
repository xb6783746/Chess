using ChessServer.Interfaces;
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
    [Serializable]
    class RemoteGamer :IGamer
    {
        public RemoteGamer()
        {
            wait = new EventWaitHandle(false, EventResetMode.AutoReset);
        }

        private IGame game;
        private Color color;
        private StepInfo step;
        [NonSerialized]
        private EventWaitHandle wait;
        private bool isWait;

        public void Step(StepInfo step)
        {
            if (isWait)
            {
                this.step = step;
                isWait = false;

                wait.Set();
            }
        }

        public System.Drawing.Color Color
        {
            get { return color; }
        }

        public void Init(IGame game, System.Drawing.Color color)
        {
            this.game = game;
            this.color = color;

        }

        public GameTemplate.Game.StepInfo MakeStep()
        {
            isWait = true;

            wait.WaitOne();

            return step;
        }
    }
}
