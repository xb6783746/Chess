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
            return ChooseStep();
        }
        public void GameOver()
        {

        }


        private StepInfo ChooseStep()
        {
            var field = this.game.Field.Clone();
            var steps = GetAllSteps(this.Color, field);

            var taskArr = new Task<StepRating>[steps.Count];

            for (int i = 0; i < taskArr.Length; i++)
            {
                var clone = field.Clone();
                var tmp = steps[i];

                var newTask = new Task<StepRating>(() =>
                    {
                        clone.MakeStep(tmp);

                        int res = RatingField(clone, 1);

                        clone.CancelStep();

                        return new StepRating(tmp, res);
                    });

                newTask.Start();

                taskArr[i] = newTask;
            }

            Task.WaitAll(taskArr);

            int maxRating = taskArr.Max(x => x.Result.Rating);

            return taskArr.First(x => x.Result.Rating == maxRating).Result.Step;
        }

        private int RatingField(IField field, int level)
        {
            int best = 0;
            bool start = true;

            FColor current = level % 2 == 0 ? this.Color : this.EnemyColor;

            var allSteps = GetAllSteps(current, field);

            foreach (var step in allSteps)
            {
                field.MakeStep(step);

                int newRating = 0;

                if (level < maxRecurs)
                {
                    newRating = RatingField(field, level + 1);
                }
                else
                {
                    newRating = rating.Rating(field);
                }

                if (start
                    || (current == FColor.White && newRating > best)
                    || (current == FColor.Black && newRating < best))
                {
                    best = newRating;
                    start = false;
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
