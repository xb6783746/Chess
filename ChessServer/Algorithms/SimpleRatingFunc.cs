using ChessServer.Interfaces;
using GameTemplate.ChessEnums;
using GameTemplate.ChessGame.ChessEnums;
using GameTemplate.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessServer.Algorithms
{
    class SimpleRatingFunc :IRatingFunc
    {
        public SimpleRatingFunc()
        {
            price = new Dictionary<ChessFType, int>()
            {
                {ChessFType.Pawn, 100},
                {ChessFType.Knight, 320},
                {ChessFType.Bishop, 330},
                {ChessFType.Rook, 500},
                {ChessFType.Queen, 900},
                {ChessFType.King, 20000}
            };
        }

        private Dictionary<ChessFType, int> price;

        public int Rating(IReadOnlyField field, FColor color)
        {
            var figures = field.GetFiguresOnBoard().Where((x) => x.Figure.Color == color);

            return figures.Sum((x) => price[x.Figure.Type]);
        }
    }
}
