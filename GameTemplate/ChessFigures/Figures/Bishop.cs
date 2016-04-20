using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GameTemplate.Interfaces;
using GameTemplate.Game;
using GameTemplate.ChessEnums;

namespace GameTemplate.ChessFigures
{
    /// <summary>
    /// Класс Слона
    /// </summary>
    [Serializable]
    class Bishop : AbstractFigure, IChessFigure
    {
        public Bishop(FColor color)
        {
            this.color = color;
        }

        public ChessFType Type
        {
            get { return ChessFType.Bishop; }
        }

        public bool Step(Point from, Point to, IReadOnlyField field)
        {
            return GetCells(from, field).Contains(to);
        }

        public List<Point> GetCells(Point location, IReadOnlyField field)
        {
            List<Point> cells = new List<Point>();

            //Cells(ref cells, location, 1, 1, field);
            //Cells(ref cells, location, -1, 1, field);
            //Cells(ref cells, location, -1, -1, field);
            //Cells(ref cells, location, 1, -1, field);

            for (int i = -1; i <= 1; i++)
            {
                for (int k = -1; k <= 1; k++)
                {
                    if (i == 0 || k == 0)
                    {
                        continue;
                    }

                    Cells(cells, location, i, k, field);
                }
            }

            return cells;
        }

        private void Cells(List<Point> cells, Point start, int stepX, int stepY, IReadOnlyField field)
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
