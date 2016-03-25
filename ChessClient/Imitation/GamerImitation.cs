using GameTemplate.Game;
using GameTemplate.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChessClient.Imitation
{
    class GamerImitation :IGamer
    {
        public GamerImitation()
        {
            handle = new EventWaitHandle(false, EventResetMode.AutoReset);
        }

        private EventWaitHandle handle;
        private StepInfo step;
        private Color color; 

        public void Init(IGame game, System.Drawing.Color color)
        {
            this.color = color;

            //InitAction();
        }

        public StepInfo MakeStep()
        {

            Step();

            handle.WaitOne();

            return this.step;

        }

        public System.Drawing.Color Color
        {
            get { return color; }
        }

        public void NewStep(StepInfo step)
        {
            this.step = step;

            handle.Set();
        }

        public event Action InitAction;
        public event Action Step;
    }
}
