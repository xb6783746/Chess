using GameTemplate.ChessGame.ChessEnums;
using GameTemplate.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTemplate.ChessGame.ChessFigures
{
    /// <summary>
    /// Пул шахматных фигур
    /// </summary>
    [Serializable]
    public class ChessFiguresPool :IChessFigureFactory
    {
        public ChessFiguresPool()
        {
            pool = new Dictionary<ChessFType, List<IChessFigure>>();

            Init();
        }


        private Dictionary<ChessFType, List<IChessFigure>> pool;

        private void Init()
        {
            pool.Add(ChessFType.Bishop, new List<IChessFigure>() {new Bishop(Color.Black), new Bishop(Color.White) });
            pool.Add(ChessFType.King, new List<IChessFigure>() { new King(Color.Black), new King(Color.White) });
            pool.Add(ChessFType.Knight, new List<IChessFigure>() { new Knight(Color.Black), new Knight(Color.White) });
            pool.Add(ChessFType.Pawn, new List<IChessFigure>() { new Pawn(Color.Black), new Pawn(Color.White) });
            pool.Add(ChessFType.Queen, new List<IChessFigure>() { new Queen(Color.Black), new Queen(Color.White) });
            pool.Add(ChessFType.Rook, new List<IChessFigure>() { new Rook(Color.Black), new Rook(Color.White) });

        }

        public IChessFigure GetFigure(ChessFType type, Color color)
        {
            return pool[type].Find(x => x.Color == color);
        }
    }
}
