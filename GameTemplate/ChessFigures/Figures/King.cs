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
    /// Класс Короля    
    /// </summary>
    [Serializable]
    class King : AbstractFigure, IChessFigure
    {
        public King(FColor color)
        {
            this.color = color;
        }

        public ChessFType Type
        {
            get { return ChessFType.King; }
        }

        public bool Step(Point from, Point to, IReadOnlyField field)
        {
            return GetCells(from, field).Contains(to);
        }
        public List<Point> GetCells(Point location, IReadOnlyField field)
        {
            return GetAllCells(location, field);
        }

        private List<Point> GetAllCells(Point location, IReadOnlyField field)
        {
            List<Point> cells = new List<Point>();

            //Cell(ref cells, location, 0, -1, field);
            //Cell(ref cells, location, 0, 1, field);
            //Cell(ref cells, location, 1, 0, field);
            //Cell(ref cells, location, -1, 0, field);

            //Cell(ref cells, location, 1, -1, field);
            //Cell(ref cells, location, 1, 1, field);
            //Cell(ref cells, location, -1, 1, field);
            //Cell(ref cells, location, -1, -1, field);

            for (int i = -1; i <= 1; i++)
            {
                for (int k = -1; k <= 1; k++)
                {
                    if (i == k && k == 0)
                    {
                        continue;
                    }

                    Cell(ref cells, location, i, k, field);
                }
            }

            return cells;
        }
        private void Cell(ref List<Point> cells, Point start, int stepX, int stepY, IReadOnlyField field)
        {
            temp = new Point(start.X + stepX, start.Y + stepY);
            if (TestPoint(temp) && (field[temp] == null || (field[temp] != null && field[temp].Color != this.color)))
            {
                cells.Add(temp);
            }
        }
    }
}
