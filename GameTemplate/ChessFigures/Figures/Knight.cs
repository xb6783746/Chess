using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameTemplate.ChessGame.ChessEnums;
using System.Drawing;
using GameTemplate.Interfaces;
using GameTemplate.ChessEnums;

namespace GameTemplate.ChessGame.ChessFigures
{
    /// <summary>
    /// Класс коня
    /// </summary>
    [Serializable]
    class Knight : IChessFigure
    {
        public Knight(FColor color)
        {
            this.color = color;
        }

        public ChessFType Type
        {
            get { return ChessFType.Knight; }
        }
        public FColor Color
        {
            get { return color; }
        }

        private FColor color;
        private Point temp;

        public bool Step(Point from, Point to, IReadOnlyField field)
        {
            return GetCells(from, field).Contains(to);
        }

        public List<Point> GetCells(Point location, IReadOnlyField field)
        {
            List<Point> cells = new List<Point>();

            Cells(ref cells, location, -2, 1, field);
            Cells(ref cells, location, -1, 2, field);
            Cells(ref cells, location,  1, 2, field);
            Cells(ref cells, location,  2, 1, field);

            return cells;
        }

        private void Cells(ref List<Point> cells, Point start, int stepX, int stepY, IReadOnlyField field)
        {
            temp = new Point(start.X + stepX, start.Y + stepY);
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

        private bool TestPoint(Point temp)
        {
            return temp.X >= 0 && temp.X < 8 && temp.Y >= 0 && temp.Y < 8;
        }
    }
}
