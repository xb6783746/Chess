using GameTemplate.ChessEnums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTemplate.ChessFigures
{
    [Serializable]
    abstract class AbstractFigure
    {
        public FColor Color
        {
            get { return color; }
        }

        protected FColor color;
        protected Point temp;

        protected bool TestPoint(Point temp)
        {
            return temp.X >= 0 && temp.X < 8 && temp.Y >= 0 && temp.Y < 8;
        }
    }
}
