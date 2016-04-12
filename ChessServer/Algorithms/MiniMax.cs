using ChessServer.Interfaces;
using GameTemplate.ChessEnums;
using GameTemplate.Game;
using GameTemplate.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessServer.Algorithms
{
    class MiniMax : IGamer
    {
        private class StepRating
        {
            public StepRating(StepInfo step, int rating)
            {
                this.Step = step;
                this.Rating = rating;
            }

            public StepInfo Step { get; set; }
            public int Rating { get; set; }

        }

        public MiniMax(IRatingFunc rating)
        {
            this.rating = rating;
        }

        private IRatingFunc rating;
        private IGame game;
        private int maxRecurs = 0;

        private FColor EnemyColor
        {
            get
            {
                return Color == FColor.Black ? FColor.White : FColor.Black;
            }
        }

        public FColor Color
        {
            get;
            private set;
        }

        public void Init(IGame game, FColor color)
        {
            this.game = game;
            this.Color = color;
        }
        public StepInfo MakeStep()
        {
            return ChooseStep();
        }
        public void GameOver()
        {

        }


        private StepInfo ChooseStep()
        {
            var field = this.game.Field.Clone();
            try
            {
                return RatingField(field, 0).Step;
            }
            catch (Exception e)
            {
                return new StepInfo(new Point(), new Point());
            }
        }

        private StepRating RatingField(IField field, int level)
        {
            StepRating best = null;

            var allSteps = GetAllSteps(this.Color, field);

            foreach (var step in allSteps)
            {
                field.MakeStep(step);

                StepRating newRating = null;

                if (level < maxRecurs)
                {
                    newRating = RatingField(field, level + 1);
                }
                else
                {
                    newRating = new StepRating(step, rating.Rating(field, EnemyColor));
                }

                if (best == null || newRating.Rating < best.Rating)
                {
                    best = newRating;
                }

                field.CancelStep();
            }

            return best;

        }
        private List<StepInfo> GetAllSteps(FColor color, IField game)
        {
            List<FigureOnBoard> myFigures =
                game
                .GetFiguresOnBoard()
                .Where((x) => x.Figure.Color == this.Color)
                .ToList();

            List<StepInfo> allSteps = new List<StepInfo>();
            foreach (var item in myFigures)
            {
                allSteps.AddRange(
                    item
                    .Figure
                    .GetCells(item.Location, game)
                    .Select((x) => new StepInfo(item.Location, x))
                    );
            }

            return allSteps;
        }
    }
}
