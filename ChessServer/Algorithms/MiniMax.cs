using AITurnScreen;
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
using System.Windows.Forms;

namespace ChessServer.Algorithms
{
    class MiniMax : IGamer
    {
        private class StepRating
        {
            public StepRating(StepInfo step, int rating, StepRating next)
            {
                this.Step = step;
                this.Rating = rating;
                //this.Field = field;
                this.Next = next;
            }
            public StepRating()
            {

            }

            public StepInfo Step { get; set; }
           // public IReadOnlyField Field { get; set; }
            public StepRating Next { get; set; }
            public int Rating { get; set; }

        }

        public MiniMax(IRatingFunc rating)
        {
            this.rating = rating;
        }

        private IRatingFunc rating;
        private IGame game;
        private int maxRecurs = 2;

        private AITurnForm form;

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


            form = new AITurnForm();
            Task.Run(() => 
                {
                    Application.EnableVisualStyles();
                    Application.Run(form);  
                });
            
        }
        public StepInfo MakeStep()
        {
            return ChooseStep();
        }
        public void GameOver()
        {
            if (form != null)
            {
                form.Close();
            }
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

                        var res = RatingField(clone, 1);

                        return new StepRating(tmp, res.Rating, res);
                    });

                newTask.Start();

                taskArr[i] = newTask;
            }

            Task.WaitAll(taskArr);

            int maxRating = taskArr.Max(x => x.Result.Rating);

            var result = taskArr.First(x => x.Result.Rating == maxRating).Result;


            List<StepInfo> stepList = new List<StepInfo>();
            var pointer = result;

            while (pointer != null)
            {
                stepList.Add(pointer.Step);
                pointer = pointer.Next;
            }

            form.Draw(stepList, field);

            return stepList[0];
        }

        private StepRating RatingField(IField field, int level)
        {
            StepRating best = new StepRating();
            bool start = true;

            FColor current = level % 2 == 0 ? this.Color : this.EnemyColor;

            var allSteps = GetAllSteps(current, field);

            foreach (var step in allSteps)
            {
                field.MakeStep(step);

                StepRating newRating;

                if (level < maxRecurs)
                {
                    newRating = RatingField(field, level + 1);
                }
                else
                {
                    newRating = new StepRating(step, rating.Rating(field), null);
                }

                if (start
                    || (current == FColor.White && newRating.Rating > best.Rating)
                    || (current == FColor.Black && newRating.Rating < best.Rating))
                {
                    best.Rating = newRating.Rating;
                    best.Step = step;
                    if (level < maxRecurs)
                    {
                        best.Next = newRating;
                    }
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
