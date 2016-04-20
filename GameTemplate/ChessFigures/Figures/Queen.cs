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
    /// Класс Ферзя
    /// </summary>
    [Serializable]
    class Queen : AbstractFigure, IChessFigure
    {
        public Queen(FColor color)
        {
            this.color = color;
        }

        public ChessFType Type
        {
            get { return ChessFType.Queen; }
        }

        public bool Step(Point from, Point to, IReadOnlyField field)
        {
            return GetCells(from, field).Contains(to);
        }
        public List<Point> GetCells(Point location, IReadOnlyField field)
        {
            List<Point> cells = new List<Point>();

            for (int i = -1; i <= 1; i++)
            {
                for (int k = -1; k <= 1; k++)
                {
                    if (i == k && k == 0)
                    {
                        continue;
                    }

                    GetCells(cells, location, i, k, field);
                }
            }

            //GetCells(ref cells, location,  1, -1, field);
            //GetCells(ref cells, location,  1,  1, field);
            //GetCells(ref cells, location, -1,  1, field);
            //GetCells(ref cells, location, -1, -1, field);

            //GetCells(ref cells, location,  0, -1, field);
            //GetCells(ref cells, location,  1,  0, field);
            //GetCells(ref cells, location,  0,  1, field);
            //GetCells(ref cells, location, -1,  0, field);

            return cells;
        }
        private void GetCells(List<Point> cells, Point start, int stepX, int stepY, IReadOnlyField field)
        {
            var temp = new Point(start.X + stepX, start.Y + stepY);

            while (TestPoint(temp) && field[temp] == null)
            {

                cells.Add(temp);

                temp.X += stepX;
                temp.Y += stepY;
            }
            if (TestPoint(temp) && field[temp] != null && field[temp].Color != this.Color)
            {
                cells.Add(temp);
            }
        }
    }
}
