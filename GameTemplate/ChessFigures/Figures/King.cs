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
    /// Класс Короля    
    /// </summary>
    [Serializable]
    class King : IChessFigure
    {
        public King(FColor color)
        {
            this.color = color;
        }

        public ChessFType Type
        {
            get { return ChessFType.King; }
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
            List<Point> cells = GetAllCells(location, field);
            //Point temp;
            //for (int i = 0; i < cells.Count; i++)
            //{
            //    temp = new Point(cells[i].X, cells[i].Y);
            //    if (field[temp] != null && field[temp].Color == color)
            //    {
            //        cells.Remove(temp);
            //    }
            //}

            return cells;
        }

        private List<Point> GetAllCells(Point location, IReadOnlyField field)
        {
            List<Point> cells = new List<Point>();
            Straight(ref cells, location, 0, -1, field);
            Straight(ref cells, location, 1, 0, field);
            Straight(ref cells, location, 0, 1, field);
            Straight(ref cells, location, -1, 0, field);

            Obliquely(ref cells, location, 1, -1, field);
            Obliquely(ref cells, location, 1, 1, field);
            Obliquely(ref cells, location, -1, 1, field);
            Obliquely(ref cells, location, -1, -1, field);
            

            return cells;
        }

        private void Straight(ref List<Point> cells, Point start, int stepX, int stepY, IReadOnlyField field)
        {
            temp = new Point(start.X + stepX, start.Y + stepY);
            if (TestPoint(temp) && (field[temp] == null || ( field[temp] != null && field[temp].Color != this.color)))
            {
                cells.Add(temp);    
            }
        }
        private void Obliquely(ref List<Point> cells, Point start, int stepX, int stepY, IReadOnlyField field)
        {
            temp = new Point(start.X + stepX, start.Y + stepY);
            if (TestPoint(temp) && field[temp] != null && field[temp].Color != this.color)
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
