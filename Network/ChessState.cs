using GameTemplate.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    [Serializable]
    public class ChessState
    {
        public ChessState(IReadOnlyList<FigureOnBoard> figures, StepInfo step)
        {
            this.Figures = figures;
            this.LastStep = step;
        }

        public IReadOnlyList<FigureOnBoard> Figures { get; private set; }
       // public FColor Color { get; private set; }
        public StepInfo LastStep { get; private set; }
    }
}
