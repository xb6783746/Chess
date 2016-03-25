﻿using System;
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
    /// Класс Ладьи
    /// </summary>
    class Rook : IChessFigure
    {
        private Color color;
        public Color Color
        {
            get { return color; }
        }

        public ChessFType Type
        {
            get { return ChessFType.Rook; }
        }
        public Rook(Color color)
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

            while (start.X < 8 && start.X >= 0 && start.Y < 8 && start.Y >= 0)
            {
                start.X += stepX;
                start.Y += stepY;

                temp.Add(start);
            }

            return temp;
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

            return cells;
        }
    }
}