using GameTemplate.Game;
using GameTemplate.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTemplate.ChessField
{
    struct Keeper
    {
        public Keeper(StepInfo step, IChessFigure attacker, IChessFigure attacked)
        {
            this.step = step;
            this.attacker = attacker;
            this.attacked = attacked;
        }

        private StepInfo step;
        private IChessFigure attacker;
        private IChessFigure attacked;

        public StepInfo Step
        {
            get { return step; }
        }
        public IChessFigure Attacker
        {
            get { return attacker; }
        }
        public IChessFigure Attacked
        {
            get { return attacked; }
        }


        

    }
}
