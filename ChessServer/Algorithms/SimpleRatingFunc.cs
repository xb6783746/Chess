using ChessServer.Interfaces;
using GameTemplate.ChessEnums;
using GameTemplate.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessServer.Algorithms
{
    class SimpleRatingFunc : IRatingFunc
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

        public int Rating(IReadOnlyField field)
        {
            return Rate(field, FColor.White) - Rate(field, FColor.Black);
        }

        //private int Rate(IReadOnlyField field, FColor color)
        //{
        //    int rating = 0;

        //    var figures = field.GetFiguresOnBoard().Where((x) => x.Figure.Color == color);
        //    var otherFigures = field.GetFiguresOnBoard().Where((x) => x.Figure.Color != color).ToList();

        //    var king = figures.FirstOrDefault((x) => x.Figure.Type == ChessFType.King);

        //    rating += figures.Where((x) => x.Figure.Type != ChessFType.King).Sum((x) => price[x.Figure.Type]);

        //    if (king.Figure != null)
        //    {
        //        if (otherFigures.Exists(x => x.Figure.Step(x.Location, king.Location, field)))
        //        {
        //            return -100000;
        //        }
        //    }

        //    foreach (var item in figures)
        //    {
        //        if (otherFigures.Exists(x => x.Figure.Step(x.Location, item.Location, field)))
        //        {
        //            rating -= price[item.Figure.Type];
        //        }
        //    }

        //    return rating;
        //}

        private int Rate(IReadOnlyField field, FColor color)
        {
            int rating = 0;

            var allFigures = field.GetFiguresOnBoard();

            var figures = allFigures.Where((x) => x.Figure.Color == color);
            var otherFigures = allFigures.Where((x) => x.Figure.Color != color).ToList();

            rating += figures.Sum(x => price[x.Figure.Type]);

            foreach (var fig in figures)
            {
                if(otherFigures.Exists( x=> x.Figure.Step(x.Location, fig.Location, field)))
                {
                    rating -= price[fig.Figure.Type];
                }
            }

            return rating;
        }
    }
}
