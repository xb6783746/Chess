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
        private int maxRecurs = 2;

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
            try
            {
                return ChooseStep();
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public void GameOver()
        {

        }


        private StepInfo ChooseStep()
        {
            var field = this.game.Field.Clone();

            var taskList = new List<Task<StepRating>>();
           // var tmp = new List<StepRating>();

            foreach (var step in GetAllSteps(this.Color, field))
            {
                var clone = field.Clone();
                var tmp = step;

                var newTask = new Task<StepRating>(() =>
                    {
                        clone.MakeStep(tmp);

                        var res = RatingField(clone, 1);
                        res.Step = tmp;

                        clone.CancelStep();

                        return res;
                    });

                taskList.Add(newTask);
            }

            taskList.ForEach(x => x.Start());
            taskList.ForEach(x => x.Wait());

            int maxRating = taskList.Max(x => x.Result.Rating);

            return taskList.First(x => x.Result.Rating == maxRating).Result.Step;
        }

        private StepRating RatingField(IField field, int level)
        {
            try
            {
                StepRating best = null;

                FColor current = level % 2 == 0 ? this.Color : this.EnemyColor;

                var allSteps = GetAllSteps(current, field);

                foreach (var step in allSteps)
                {
                    field.MakeStep(step);

                    StepRating newRating = null;

                    if (level < maxRecurs)
                    {
                        newRating = RatingField(field, level + 1);
                        newRating.Step = step;
                    }
                    else
                    {
                        newRating = new StepRating(step, rating.Rating(field));
                    }

                    if (best == null
                        || (current == FColor.White && newRating.Rating > best.Rating)
                        || (current == FColor.Black && newRating.Rating < best.Rating))
                    {
                        best = newRating;
                    }

                    field.CancelStep();
                }

                return best;
            }
            catch
            {
                throw;
            }

        }
        private List<StepInfo> GetAllSteps(FColor color, IField game)
        {
            List<FigureOnBoard> myFigures =
                game
                .GetFiguresOnBoard()
                .Where((x) => x.Figure.Color == color)
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
