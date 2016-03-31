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
    /// Класс пешки
    /// </summary>
    [Serializable]
    class Pawn : IChessFigure
    {
        public Pawn(FColor color)
        {
            this.color = color;
        }

        private FColor color;

        public FColor Color
        {
            get { return color; }
        }
        public ChessFType Type
        {
            get { return ChessFType.Pawn; }
        }

        public bool Step(Point from, Point to, IReadOnlyField field)
        {
            return GetCells(from, field).Contains(to);
        }
        private List<Point> GetAllCells(Point location)
        {
            List<Point> cells = new List<Point>();

            if (location.Y - 1 >= 0)
            {
                cells.Add(new Point(location.X, location.Y - 1));
            }
            if (location.Y + 1 <= 7)
            {
                cells.Add(new Point(location.X, location.Y + 1));
            }

            return cells;
        }
        public List<Point> GetCells(Point location, IReadOnlyField field)
        {
            List<Point> cells = GetAllCells(location);
            Point temp;

            for (int i = 0; i < cells.Count; i++)
            {
                temp = cells[i];
                if (field[temp] != null && field[temp].Color == color)
                {
                    cells.Remove(temp);
                }
            }

            //Cell(ref cells, location, field, -1);
            //Cell(ref cells, location, field, 1);

            return cells;
        }

        private void Cell(ref List<Point> cells, Point location, IReadOnlyField field, int stepX)
        {
            Point temp = new Point(location.X + stepX, location.Y + 1);
            if (temp.X >= 0 && temp.Y < 8 && field[temp].Color != color)
            {
                cells.Add(temp);
            }
        }
    }
}
