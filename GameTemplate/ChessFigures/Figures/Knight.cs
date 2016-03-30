using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameTemplate.ChessGame.ChessEnums;
using System.Drawing;
using GameTemplate.Interfaces;

namespace GameTemplate.ChessGame.ChessFigures
{
    /// <summary>
    /// Класс коня
    /// </summary>
    class Knight : IChessFigure
    {
        public Knight(Color color)
        {
            this.color = color;
        }

        private Color color;

        public ChessFType Type
        {
            get { return ChessFType.Knight; }
        }
        public Color Color
        {
            get { return color; }
        }
    
        public bool Step(Point from, Point to, IReadOnlyField field)
        {
            return GetCells(from, field).Contains(to);
        }
        public List<Point> GetAllCells(Point location)
        {
            List<Point> cells = new List<Point>();

            cells.AddRange(Cells(location, 2, 1));
            cells.AddRange(Cells(location, 1, 2));
            cells.AddRange(Cells(location, -1, 2));
            cells.AddRange(Cells(location, -2, 1));

            return cells;
        }
        public List<Point> GetCells(Point location, IReadOnlyField field)
        {
            List<Point> cells = GetAllCells(location);
            Point temp;
            for (int i = 0; i < cells.Count; i++)
            {
                temp = new Point(cells[i].X, cells[i].Y);
                if (field[temp].Color == color)
                {
                    cells.Remove(temp);
                }
            }

            return cells;
        }

        private List<Point> Cells(Point start, int stepX, int shift) /// ????
        {
            List<Point> temp = new List<Point>();
            if (start.X + stepX < 8)
            {
                if (start.Y + shift < 8)
                {
                    temp.Add(new Point(start.X + stepX, start.Y + shift));
                }

                if (start.Y - shift >= 0)
                {
                    temp.Add(new Point(start.X + stepX, start.Y - shift));
                }
            }

            return temp;
        }
    }
}
