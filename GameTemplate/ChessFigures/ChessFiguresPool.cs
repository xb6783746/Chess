using GameTemplate.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameTemplate.ChessEnums;

namespace GameTemplate.ChessFigures
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
            pool.Add(ChessFType.Bishop, new List<IChessFigure>() { new Bishop(FColor.Black), new Bishop(FColor.White) });
            pool.Add(ChessFType.King, new List<IChessFigure>() { new King(FColor.Black), new King(FColor.White) });
            pool.Add(ChessFType.Knight, new List<IChessFigure>() { new Knight(FColor.Black), new Knight(FColor.White) });
            pool.Add(ChessFType.Pawn, new List<IChessFigure>() { new Pawn(FColor.Black), new Pawn(FColor.White) });
            pool.Add(ChessFType.Queen, new List<IChessFigure>() { new Queen(FColor.Black), new Queen(FColor.White) });
            pool.Add(ChessFType.Rook, new List<IChessFigure>() { new Rook(FColor.Black), new Rook(FColor.White) });

        }

        public IChessFigure GetFigure(ChessFType type, FColor color)
        {
            return pool[type].Find(x => x.Color == color);
        }

    }
}
