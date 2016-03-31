using GameTemplate.ChessEnums;
using GameTemplate.ChessGame.ChessEnums;
using GameTemplate.ChessGame.ChessFigures;
using GameTemplate.Game;
using GameTemplate.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTemplate.ChessGame.ChessField
{
    /// <summary>
    /// Поле для шахмат
    /// </summary>
    [Serializable]
    public class ChessField :IField
    {
        public ChessField(IChessFigureFactory factory)
        {
            this.factory = factory;

            field = new IChessFigure[8, 8];
            diedFigures = new Dictionary<IChessFigure, int>();

            Init();
        }

        public static IReadOnlyField Empty
        {
            get
            {
                return new ChessField(new ChessFiguresPool());
            }
        }

        private IChessFigure[,] field;
        [NonSerialized]
        private IChessFigureFactory factory;
        private Dictionary<IChessFigure, int> diedFigures;

        public IChessFigure this[Point location]
        {
            get { return field[location.X, location.Y]; }
            private set { field[location.X, location.Y] = value; }
        }
        public bool IsGameOver
        {
            get;
            private set;
        }

        public IReadOnlyList<FigureOnBoard> GetFiguresOnBoard()
        {
            List<FigureOnBoard> res = new List<FigureOnBoard>();

            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int k = 0; k < field.GetLength(1); k++)
                {
                    if (field[i, k] != null)
                    {
                        res.Add(new FigureOnBoard(field[i, k], new Point(i, k)));
                    }
                }
            }

            return res.AsReadOnly();
        }
        public IReadOnlyDictionary<IChessFigure, int> GetDiedFigures()
        {
            return diedFigures;
        }
        public bool MakeStep(Point from, Point to)
        {
            IChessFigure attacker = this[from];
            IChessFigure attacked = this[to];

            if (attacker.Step(from, to, this) && (attacked == null || attacker.Color != attacked.Color))
            {
                this[to] = attacker;
                this[from] = null;

                if (attacked != null)
                {
                    Died(attacked);

                    if (attacked.Type == ChessFType.King)
                    {
                        IsGameOver = true;
                        GameOver(attacker.Color);
                    }
                }
               
                return true;
            }

            return false;
        }
        public bool MakeStep(StepInfo step)
        {
           return MakeStep(step.From, step.To);
        }
        public IReadOnlyField GetReadOnlyField()
        {
            return this;
        }

        public event Action<FColor> GameOver;

        private void Init()
        {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                field[i, 1] = factory.GetFigure(ChessFType.Pawn, FColor.Black);
                field[i, 6] = factory.GetFigure(ChessFType.Pawn, FColor.White);
            }

            field[0, 0] = field[7, 0] = factory.GetFigure(ChessFType.Rook, FColor.Black);
            field[0, 7] = field[7, 7] = factory.GetFigure(ChessFType.Rook, FColor.White);

            field[1, 0] = field[6, 0] = factory.GetFigure(ChessFType.Knight, FColor.Black);
            field[1, 7] = field[6, 7] = factory.GetFigure(ChessFType.Knight, FColor.White);

            field[2, 0] = field[5, 0] = factory.GetFigure(ChessFType.Bishop, FColor.Black);
            field[2, 7] = field[5, 7] = factory.GetFigure(ChessFType.Bishop, FColor.White);

            field[3, 0] = factory.GetFigure(ChessFType.Queen, FColor.Black);
            field[3, 7] = factory.GetFigure(ChessFType.Queen, FColor.White);

            field[4, 0] = factory.GetFigure(ChessFType.King, FColor.Black);
            field[4, 7] = factory.GetFigure(ChessFType.King, FColor.White);
        }
        private void Died(IChessFigure died)
        {

            if (!diedFigures.ContainsKey(died))
            {
                diedFigures.Add(died, 0);
            }

            diedFigures[died]++;
        }


    }
}
