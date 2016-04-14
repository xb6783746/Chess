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
    /// Класс пешки
    /// </summary>
    [Serializable]
    class Pawn : IChessFigure
    {
        public Pawn(FColor color)
        {
            this.color = color;
        }

        public FColor Color
        {
            get { return color; }
        }
        public ChessFType Type
        {
            get { return ChessFType.Pawn; }
        }

        private FColor color;
        private Point temp;

        public bool Step(Point from, Point to, IReadOnlyField field)
        {
            return GetCells(from, field).Contains(to);
        }
        public List<Point> GetCells(Point location, IReadOnlyField field)
        {
            return this.color == FColor.White ? GetTurnOfWhite(location, field) : GetTurnOfBlack(location, field);
        }

        private List<Point> GetTurnOfWhite(Point location, IReadOnlyField field)
        {
            List<Point> cells = new List<Point>();
            temp = new Point(location.X, location.Y - 1);

            if (temp.Y >= 0)
            {
                do
                {
                    if (field[temp] == null)
                    {
                        cells.Add(temp);
                    }
                    else
                    {
                        break;
                    }

                    temp.Y--;

                } while (temp.Y > 3);
            }

            EnemyFigure(ref cells, location, field, -1, -1);
            EnemyFigure(ref cells, location, field, 1, -1);


            return cells;
        }
        private List<Point> GetTurnOfBlack(Point location, IReadOnlyField field)
        {
            List<Point> cells = new List<Point>();
            temp = new Point(location.X, location.Y + 1);
            if (temp.Y < 8)
            {
                do
                {
                    if (field[temp] == null)
                    {
                        cells.Add(temp);
                    }
                    else
                    {
                        break;
                    }

                    temp.Y++;

                } while (temp.Y <= 3);
            }

            EnemyFigure(ref cells, location, field, -1, 1);
            EnemyFigure(ref cells, location, field, 1, 1);

            return cells;
        }

        private void EnemyFigure(ref List<Point> cells, Point location, IReadOnlyField field, int stepX, int stepY)
        {
            temp = new Point(location.X + stepX, location.Y + stepY);
            if (TestPoint(temp) && field[temp] != null && field[temp].Color != this.color)
            {
                cells.Add(temp);
            }
        }
        private bool TestPoint(Point temp)
        {
            return temp.X >= 0 && temp.X < 8 && temp.Y < 8 && temp.Y >= 0;
        }
    }
}
