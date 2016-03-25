using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientAPI;
using GameTemplate.ChessGame.ChessEnums;
using GameTemplate.Interfaces;
using System.Drawing;
using GameTemplate.ChessGame.ChessInterfaces;
using Rendering.Properties;
using Rendering.Exceptions;

namespace Rendering
{
    public class Render : IRender
    {
        private Dictionary<Type, Image> figurePictures;
        private float blockSize;

        public Render(int wh, int ht)
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
        }
        public void UpdateField(Bitmap bitmap, IReadOnlyField field)
        {
            blockSize = bitmap.Size.Height / 8 - 0.1f;
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
                DrawGrid(g);
                DrawFigures(g, field);
            }
        }
        private void DrawFigures(Graphics g, IReadOnlyField field)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    IChessFigure figure = field[new Point(i, j)];
                    g.DrawImage(figurePictures[new Type(figure.Type, figure.Color)], new PointF(i * blockSize, j * blockSize));
                }
            }
        }
        private void DrawGrid(Graphics g)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    g.DrawRectangle(Pens.Black, blockSize * i, blockSize * j, blockSize, blockSize);
                }
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
