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
        }

        private Dictionary<Type, Image> figurePictures;
        private Dictionary<FColor, Color> fColor;
        private float blockSize;
        private Pen penGreen;
        private Pen penRed;
        private Pen penBlue;

        public void UpdateField(Bitmap bitmap, IReadOnlyList<FigureOnBoard> field)
        {
            blockSize = bitmap.Size.Height / 8 - 0.1f;
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
                DrawGrid(g);
                DrawFigures(g, field);
            }
        }
        public void DrawCells(Bitmap bitmap, IReadOnlyField field, Point from, List<Point> cells)
        {
            blockSize = bitmap.Size.Height / 8 - 0.1f;
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
                DrawGrid(g);
                DrawFigures(g, field);
                DrawCells(g, field, from, cells);
            }
        }

        private void DrawCells(Graphics g, IReadOnlyField field, Point from, List<Point> cell)
        {
            for (int i = 0; i < cell.Count; i++)
            {
                if (field[cell[i]] == null)
                {
                    g.DrawRectangle(penGreen, cell[i].X * blockSize, cell[i].Y * blockSize, blockSize, blockSize);
                }
                else
                {
                    g.DrawRectangle(penRed, cell[i].X * blockSize, cell[i].Y * blockSize, blockSize, blockSize);
                }
            }
            g.DrawRectangle(penBlue, from.X * blockSize, from.Y * blockSize, blockSize, blockSize);
        }
        private void DrawFigures(Graphics g, IReadOnlyList<FigureOnBoard> field)
        {
            foreach (var item in field)
            {
                var type = new Type(item.Figure.Type, fColor[item.Figure.Color]);
                g.DrawImage(figurePictures[type], new PointF(item.Location.X * blockSize, item.Location.Y * blockSize));
            }

        }
        private void DrawGrid(Graphics g)
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

        private void DrawFigures(Graphics g, IReadOnlyField field)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    IChessFigure figure = field[new Point(i, j)];
                    if (figure != null)
                    {
                        var type = new Type(figure.Type, fColor[figure.Color]);
                        g.DrawImage(figurePictures[type], new PointF(i * blockSize, j * blockSize));

                    }

                }
            }
        }

        public void UpdateField(Bitmap bitmap, IReadOnlyList<FigureOnBoard> field, StepInfo step)
        {
            UpdateField(bitmap, field);

            using (Graphics g = Graphics.FromImage(bitmap))
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(50, Color.Purple)))
            {
                g.FillRectangle(brush, step.From.X * blockSize, step.From.Y * blockSize, blockSize, blockSize);
                g.FillRectangle(brush, step.To.X * blockSize, step.To.Y * blockSize, blockSize, blockSize);
            }
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
