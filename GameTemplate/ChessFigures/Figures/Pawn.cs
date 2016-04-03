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
            List<Point> cells = this.color == FColor.White ? GetTurnOfWhite(location, field) : GetTurnOfBlack(location, field);
            Point temp;

            for (int i = 0; i < cells.Count; i++)
            {
                temp = cells[i];
                if (field[temp] != null && field[temp].Color == color)
                {
                    cells.Remove(temp);
                }
            }

            return cells;
        }

        private List<Point> GetTurnOfWhite(Point location, IReadOnlyField field)
        {
            List<Point> cells = new List<Point>();

            if (location.Y - 1 <= 7)
            {
                cells.Add(new Point(location.X, location.Y - 1));
                EnemyFigure(ref cells, location, field, -1, -1);
                EnemyFigure(ref cells, location, field, 1, -1);
            }

            return cells;
        }
        private List<Point> GetTurnOfBlack(Point location, IReadOnlyField field)
        {
            List<Point> cells = new List<Point>();

            if (location.Y + 1 >= 0)
            {
                cells.Add(new Point(location.X, location.Y + 1));
                EnemyFigure(ref cells, location, field, -1, 1);
                EnemyFigure(ref cells, location, field, 1, 1);
            }

            return cells;
        }

        private void EnemyFigure(ref List<Point> cells, Point location, IReadOnlyField field, int stepX, int stepY)
        {
            temp = new Point(location.X + stepX, location.Y + stepY);
            if (temp.X >= 0 && temp.X < 8 && temp.Y < 8 && temp.Y >= 0 && field[temp] != null && field[temp].Color != this.color)
            {
                cells.Add(temp);
            }
        }
    }
}
