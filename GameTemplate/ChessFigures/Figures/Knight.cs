using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GameTemplate.Interfaces;
using GameTemplate.ChessEnums;

namespace GameTemplate.ChessFigures
{
    /// <summary>
    /// Класс коня
    /// </summary>
    [Serializable]
    class Knight : AbstractFigure, IChessFigure
    {
        public Knight(FColor color)
        {
            this.color = color;
        }

        public ChessFType Type
        {
            get { return ChessFType.Knight; }
        }

        public bool Step(Point from, Point to, IReadOnlyField field)
        {
            return GetCells(from, field).Contains(to);
        }
        public List<Point> GetCells(Point location, IReadOnlyField field)
        {
            List<Point> cells = new List<Point>();

            Cells(cells, location, -2, 1, field);
            Cells(cells, location, -1, 2, field);
            Cells(cells, location,  1, 2, field);
            Cells(cells, location,  2, 1, field);

            return cells;
        }

        private void Cells(List<Point> cells, Point start, int stepX, int stepY, IReadOnlyField field)
        {
            var temp = new Point(start.X + stepX, start.Y + stepY);
            if (TestPoint(temp) && (field[temp] == null || (field[temp] != null && field[temp].Color != this.color)))
            {
                cells.Add(temp);
            }

            temp = new Point(start.X + stepX, start.Y - stepY);
            if (TestPoint(temp) && (field[temp] == null || (field[temp] != null && field[temp].Color != this.color)))
            {
                cells.Add(temp);
            }

        }
    }
}
