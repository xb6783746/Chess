using GameTemplate.ChessGame.ChessEnums;
using GameTemplate.ChessGame.ChessInterfaces;
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
    public class ChessFiguresPool :IChessFigureFactory
    {
        public ChessFiguresPool()
        {
            pool = new Dictionary<ChessFType, List<IChessFigure>>();
        }


        private Dictionary<ChessFType, List<IChessFigure>> pool;

        private void Init()
        {
            
        }

        public IChessFigure GetFigure(ChessFType type, Color color)
        {
            return pool[type].Find(x => x.Color == color);
        }
    }
}
