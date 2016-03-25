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

namespace Rendering
{
    public class Render : IRender
    {
        private Graphics g;
        private Bitmap screen;
        private Dictionary<Type, Image> figurePictures;
        private float blockSize;

        public Render(int wh, int ht)
        {
            screen = new Bitmap(wh, ht);
            blockSize = screen.Size.Height / 8 - 0.1f;
            try
            {
                figurePictures = new Dictionary<Type, Image>()
                {
                    { new Type(ChessFType.King, Color.White), Image.FromFile(@"ChessFigures\KingWhite.png") },
                    { new Type(ChessFType.Queen, Color.White), Image.FromFile(@"ChessFigures\QueenWhite.png") },
                    { new Type(ChessFType.Rook, Color.White), Image.FromFile(@"ChessFigures\RookWhite.png") },
                    { new Type(ChessFType.Bishop, Color.White), Image.FromFile(@"ChessFigures\BishopWhite.png") },
                    { new Type(ChessFType.Knight, Color.White), Image.FromFile(@"ChessFigures\KnightWhite.png") },
                    { new Type(ChessFType.Pawn, Color.White), Image.FromFile(@"ChessFigures\PawnWhite.png") },
                    { new Type(ChessFType.King, Color.Black), Image.FromFile(@"ChessFigures\KingBlack.png") },
                    { new Type(ChessFType.Queen, Color.Black), Image.FromFile(@"ChessFigures\QueenBlack.png") },
                    { new Type(ChessFType.Rook, Color.Black), Image.FromFile(@"ChessFigures\RookBlack.png") },
                    { new Type(ChessFType.Bishop, Color.Black), Image.FromFile(@"ChessFigures\BishopBlack.png") },
                    { new Type(ChessFType.Knight, Color.Black), Image.FromFile(@"ChessFigures\KnightBlack.png") },
                    { new Type(ChessFType.Pawn, Color.Black), Image.FromFile(@"ChessFigures\PawnBlack.png") },
                };
            }
            catch
            {
                throw new DataLoadException();
            }
            g = Graphics.FromImage(screen);
        }
        public void UpdateField(Bitmap bitmap, IField field)
        {
            g.Clear(Color.White);
            DrawGrid();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    IChessFigure figure = field[new Point(i, j)];
                    g.DrawImage(figurePictures[new Type(figure.Type, figure.Color)], new PointF(i * blockSize, j * blockSize));
                }
            }
        }
        private void DrawGrid()
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
        public ChessFType ChessType { get { return chessType; } }
        private Color color;
        public Color Color { get { return color; } }
        public Type(ChessFType chessType, Color color)
        {
            this.chessType = chessType;
            this.color = color;
        }
    }
}
