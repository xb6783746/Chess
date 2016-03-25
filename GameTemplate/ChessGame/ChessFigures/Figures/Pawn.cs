using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameTemplate.ChessGame.ChessInterfaces;
using GameTemplate.ChessGame.ChessEnums;
using System.Drawing;

namespace GameTemplate.ChessGame.ChessFigures
{
    /// <summary>
    /// Класс пешки
    /// </summary>
    class Pawn : IChessFigure
    {
        private Color color;
        public Color Color
        {
            get { return color; }
        }
        public ChessFType Type
        {
            get { return ChessFType.Pawn; }
        }
        public Pawn(Color color)
        {
            this.color = color;
        }


        public bool Step(Point from, Point to, Interfaces.IField<IChessFigure> field)
        {
            return GetCells(from, field).Contains(to);
        }

        public List<Point> GetAllCells(Point location)
        {
            List<Point> cells = new List<Point>();

            if (location.X + 1 < 8)
                cells.Add(new Point(location.X + 1, location.Y));

            return cells;
        }

        public List<Point> GetCells(Point location, Interfaces.IField<IChessFigure> field)
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

            Cell(ref cells, location, field, -1);
            Cell(ref cells, location, field, 1);

            return cells;
        }
        private void Cell(ref List<Point> cells, Point location, Interfaces.IField<IChessFigure> field, int stepX)
        {
            Point temp = new Point(location.X + stepX, location.Y + 1);
            if (temp.X >= 0 && temp.Y < 8 && field[temp].Color != color)
            {
                cells.Add(temp);
            }
        }
    }
}
