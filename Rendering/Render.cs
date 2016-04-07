using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientAPI;
using GameTemplate.ChessGame.ChessEnums;
using GameTemplate.Interfaces;
using System.Drawing;
using Rendering.Properties;
using Rendering.Exceptions;
using GameTemplate.Game;
using GameTemplate.ChessEnums;

namespace Rendering
{
    public class Render : IRender
    {
        public Render()
        {
            try
            {
                figurePictures = new Dictionary<Type, Image>()
                {
                    { new Type(ChessFType.King, Color.White), Resources.KingWhite },
                    { new Type(ChessFType.Queen, Color.White), Resources.QueenWhite },
                    { new Type(ChessFType.Rook, Color.White), Resources.RookWhite },
                    { new Type(ChessFType.Bishop, Color.White), Resources.BishopWhite },
                    { new Type(ChessFType.Knight, Color.White), Resources.KnightWhite },
                    { new Type(ChessFType.Pawn, Color.White), Resources.PawnWhite },
                    { new Type(ChessFType.King, Color.Black), Resources.KingBlack },
                    { new Type(ChessFType.Queen, Color.Black), Resources.QueenBlack },
                    { new Type(ChessFType.Rook, Color.Black), Resources.RookBlack },
                    { new Type(ChessFType.Bishop, Color.Black), Resources.BishopBlack },
                    { new Type(ChessFType.Knight, Color.Black), Resources.KnightBlack },
                    { new Type(ChessFType.Pawn, Color.Black), Resources.PawnBlack },
                };
            }
            catch
            {
                throw new DataLoadException();
            }

            fColor = new Dictionary<FColor, Color>()
            {
                {FColor.Black, Color.Black},
                {FColor.White, Color.White}
            };

            penGreen = new Pen(Color.LightGreen, 3.0f);
            penRed = new Pen(Color.Red, 3.0f);
            penBlue = new Pen(Color.BlueViolet, 3.0f);
            penPurple = new Pen(Color.Purple, 3.0f);
                     
        }

        private Dictionary<Type, Image> figurePictures;
        private Dictionary<FColor, Color> fColor;
        private int blockSize;
        private Pen penGreen;
        private Pen penRed;
        private Pen penBlue;
        private Pen penPurple;
        private Bitmap back;
        private Bitmap tempField;
        private Bitmap tempCells;
        private FColor color;

        public void Init(int wh, int ht, FColor color)
        {
            blockSize = ht / 8;
            back = new Bitmap(wh, ht);
            tempField = new Bitmap(wh, ht);
            tempCells = new Bitmap(wh, ht);
            this.color = color;

            DrawGrid();
        }
        public Bitmap UpdateField(IReadOnlyList<FigureOnBoard> field)
        {
            using (Graphics g = Graphics.FromImage(tempField))
            {
                g.Clear(Color.White);
                g.DrawImage(back, 0, 0);
                DrawFigures(g, field);
            }
            return tempField;
        }
        public Bitmap DrawCells(IReadOnlyField field, Point from, List<Point> cells)
        {
            using (Graphics g = Graphics.FromImage(tempCells))
            {
                g.Clear(Color.White);
                g.DrawImage(tempField, 0, 0);
                DrawCells(g, field, from, cells);
            }
            return tempCells;
            
        }
        public Bitmap UpdateField(IReadOnlyList<FigureOnBoard> field, StepInfo step)
        {
            
            UpdateField(field);

            using (Graphics g = Graphics.FromImage(tempField))
            {
               // g.DrawRectangle(penPurple, step.From.X * blockSize, step.From.Y * blockSize, blockSize, blockSize);
                g.DrawRectangle(penPurple, GetCellRect(step.From));
                g.DrawRectangle(penPurple, GetCellRect(step.To));
            }
            return tempField;
        }

        private void DrawCells(Graphics g, IReadOnlyField field, Point from, List<Point> cell)
        {
            for (int i = 0; i < cell.Count; i++)
            {
                //if (field[cell[i]] == null)
                //{
                //    g.DrawRectangle(penGreen, cell[i].X * blockSize, cell[i].Y * blockSize, blockSize, blockSize);
                //}
                //else
                //{
                //    g.DrawRectangle(penRed, cell[i].X * blockSize, cell[i].Y * blockSize, blockSize, blockSize);
                //}
                var tmp = field[cell[i]] == null ? penGreen : penRed;

                g.DrawRectangle(tmp, GetCellRect(cell[i]));

            }

            g.DrawRectangle(penBlue, from.X * blockSize, from.Y * blockSize, blockSize, blockSize);
        }
        private void DrawFigures(Graphics g, IReadOnlyList<FigureOnBoard> field)
        {
            foreach (var item in field)
            {
                var type = new Type(item.Figure.Type, fColor[item.Figure.Color]);
                //g.DrawImage(figurePictures[type], item.Location.X * blockSize, item.Location.Y * blockSize);
                g.DrawImage(figurePictures[type], GetCellRect(item.Location));
            }

        }
        private void DrawGrid()
        {
            using (Graphics g = Graphics.FromImage(back))
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if ((i % 2 != 0 && j % 2 == 0) || (i % 2 == 0 && j % 2 != 0))
                        {
                            g.FillRectangle(Brushes.LightGray, i * blockSize, j * blockSize, blockSize, blockSize);
                        }
                        else
                        {
                            g.FillRectangle(Brushes.White, i * blockSize, j * blockSize, blockSize, blockSize);
                        }
                    }
                }
            }
        }
        private void DrawFigures(Graphics g, IReadOnlyField field)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    IChessFigure figure = field[i, j];
                    if (figure != null)
                    {
                        var type = new Type(figure.Type, fColor[figure.Color]);
                        g.DrawImage(figurePictures[type], Transform(new Point(i * blockSize, j * blockSize)));
                    }
                }
            }
        }

        private Rectangle GetCellRect(Point location)
        {
            return GetCellRect(location.X, location.Y);
        }
        private Rectangle GetCellRect(int x, int y)
        {
            if (color == FColor.Black)
            {
                y = 7 - y;
            }

            return new Rectangle(
                x * blockSize,
                y * blockSize,
                blockSize,
                blockSize
                );
        }

        private Point Transform(Point p)
        {
            if (color == FColor.Black)
            {
                p.Y = 7 - p.Y;
            }

            return p;
        }

    }

    public struct Type
    {
        private ChessFType chessType;
        private Color color;

        public ChessFType ChessType { get { return chessType; } }
        public Color Color { get { return color; } }
        public Type(ChessFType chessType, Color color)
        {
            this.chessType = chessType;
            this.color = color;
        }
    }
}
