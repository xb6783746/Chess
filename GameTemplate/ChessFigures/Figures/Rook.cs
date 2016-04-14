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
    /// Класс Ладьи
    /// </summary>
    [Serializable]
    class Rook : AbstractFigure, IChessFigure
    {
        public Rook(FColor color)
        {
            this.color = color;
        }
        public ChessFType Type
        {
            get { return ChessFType.Rook; }
        }

        public bool Step(Point from, Point to, IReadOnlyField field)
        {
            return GetCells(from, field).Contains(to);
        }
        public List<Point> GetCells(Point location, IReadOnlyField field)
        {
            List<Point> cells = new List<Point>();

            GetCells(cells, location, 0, -1, field);
            GetCells(cells, location, 0, 1, field);
            GetCells(cells, location, 1, 0, field);
            GetCells(cells, location, -1, 0, field);

            return cells;
        }
        private void GetCells(List<Point> cells, Point start, int stepX, int stepY, IReadOnlyField field)
        {
            temp = new Point(start.X + stepX, start.Y + stepY);

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
