using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameTemplate.ChessGame.ChessInterfaces;
using GameTemplate.ChessGame.ChessEnums;
using System.Drawing;
using GameTemplate.Interfaces;

namespace GameTemplate.ChessGame.ChessFigures
{
    /// <summary>
    /// Класс Короля    
    /// </summary>
    class King : IChessFigure
    {
        private Color color;
        public ChessFType Type
        {
            get { return ChessFType.King; }
        }
        public Color Color
        {
            get { return color; }
        }
        public King(Color color)
        {
            this.color = color;
        }

        public bool Step(Point from, Point to, IField field)
        {
            return GetCells(from, field).Contains(to);
        }

        public List<Point> GetAllCells(Point location)
        {
            List<Point> cells = new List<Point>();
            if (location.X - 1 >= 0)
            {
                cells.Add(new Point(location.X - 1, location.Y));
            }

            if (location.X + 1 < 8)
            {
                cells.Add(new Point(location.X + 1, location.Y));
            }
            cells.AddRange(Cells(location, 1));
            cells.AddRange(Cells(location, -1));
            return cells;
        }

        private List<Point> Cells(Point start, int stepY)
        {
            List<Point> temp = new List<Point>();
            if (start.Y + stepY < 0 || start.Y + stepY > 7)
            {
                return temp;
            }

            for (int i = -1; i < 2; i += 2)
            {
                if (start.X + i >= 0 && start.X + i < 8)
                {
                    temp.Add(new Point(start.X + i, start.Y + stepY));
                }
            }

            return temp;
        }

        public List<Point> GetCells(Point location, IField field)
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
    }
}
