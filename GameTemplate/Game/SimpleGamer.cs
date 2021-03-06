﻿using GameTemplate.ChessEnums;
using GameTemplate.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTemplate.Game
{
    public class SimpleGamer :IGamer
    {

        private FColor color;
        private IGame game;

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

            FigureOnBoard fig = figures.FirstOrDefault((x) => x.Figure.GetCells(x.Location, game.Field).Count > 0);

            if (fig.Figure != null)
            {
                Point to = fig.Figure.GetCells(fig.Location, game.Field)[0];

                return new StepInfo(fig.Location, to);
            }

            throw new Exception();
        }


        public void GameOver(FColor winner)
        {
            throw new NotImplementedException();
        }


        public void GameOver()
        {
            throw new NotImplementedException();
        }
    }
}
