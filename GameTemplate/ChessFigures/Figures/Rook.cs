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
    /// Класс Ладьи
    /// </summary>
    [Serializable]
    class Rook : IChessFigure
    {
        public Rook(FColor color)
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
            get { return ChessFType.Rook; }
        }

        public bool Step(Point from, Point to, IReadOnlyField field)
        {
            return GetCells(from, field).Contains(to);
        }
        public List<Point> GetCells(Point location, IReadOnlyField field)
        {
            List<Point> cells = GetAllCells(location);
            Point temp;
            for (int i = 0; i < cells.Count; i++)
            {
                temp = new Point(cells[i].X, cells[i].Y);
                if (field[temp] != null && field[temp].Color == color)
                {
                    cells.Remove(temp);
                }
            }

            return cells;
        }
        private List<Point> GetAllCells(Point location)
        {
            List<Point> cells = new List<Point>();

            cells.AddRange(Cells(location, 0, 1));
            cells.AddRange(Cells(location, 1, 0));
            cells.AddRange(Cells(location, 0, -1));
            cells.AddRange(Cells(location, -1, 0));

            return cells;
        }

        private List<Point> Cells(Point start, int stepX, int stepY)
        {
            List<Point> temp = new List<Point>();
            start.X += stepX;
            start.Y += stepY;

            while (start.X + stepX < 8 && start.X + stepX >= 0 && start.Y + stepY < 8 && start.Y + stepY >= 0)
            {
                start.X += stepX;
                start.Y += stepY;

                temp.Add(start);
            }

            return temp;
        }

    }
}
