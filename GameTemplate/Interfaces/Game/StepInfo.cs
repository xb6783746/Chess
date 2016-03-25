using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTemplate.Game
{
    /// <summary>
    /// Информация о ходе
    /// </summary>
    public class StepInfo
    {
        public StepInfo(Point from, Point to)
        {
            this._from = from;
            this._to = to;
        }

        private Point _from;
        private Point _to;

        public Point From { get { return _from; } }
        public Point To { get { return _to; } }
    }
}
