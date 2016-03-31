using GameTemplate.ChessEnums;
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
        public ChessState(IReadOnlyList<FigureOnBoard> figures, StepInfo step, FColor turn)
        {
            this.Figures = figures;
            this.LastStep = step;
            this.Turn = turn;
        }

        public IReadOnlyList<FigureOnBoard> Figures { get; private set; }
        public FColor Turn { get; private set; }
        public StepInfo LastStep { get; private set; }
    }
}
