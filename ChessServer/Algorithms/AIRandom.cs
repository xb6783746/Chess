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
    class AIRandom : IGamer
    {
        private FColor color;
        private IGame game;
        private StepInfo step;
        private Dictionary<FigureOnBoard, List<Point>> fig;
        private int indexRandomFigure;
        private FigureOnBoard randomFigure;
        private Point randomCell;
        private Random rand;

        public FColor Color
        {
            get { return color; }
        }

        public void Init(IGame game, FColor color)
        {
            this.game = game;
            this.color = color;
        }

        public StepInfo MakeStep()
        {
            List<FigureOnBoard> figures = game.Field.GetFiguresOnBoard().Where((x) => x.Figure.Color == color).ToList();
            List<Point> cells;
            foreach (var item in figures)
            {
                cells = item.Figure.GetCells(item.Location, game.Field);
                if (cells.Count != 0)
                {
                    fig.Add(item, cells);
                }
            }

            indexRandomFigure = rand.Next(0, fig.Keys.Count);

            randomFigure = fig.Keys.ToList()[indexRandomFigure];

            cells = fig.Values.ToList()[indexRandomFigure];
            randomCell = cells[rand.Next(0, cells.Count)];


            return new StepInfo(randomFigure.Location, randomCell);
        }

        public void GameOver()
        {

        }
    }
}