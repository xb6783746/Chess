using GameTemplate.ChessEnums;
using GameTemplate.ChessField;
using GameTemplate.ChessFigures;
using GameTemplate.Game;
using GameTemplate.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTemplate.ChessField
{
    /// <summary>
    /// Поле для шахмат
    /// </summary>
    [Serializable]
    public class ChessField :IField
    {
        protected ChessField()
        {
            field = new IChessFigure[8, 8];
            history = new Stack<Keeper>();

            readOnly = new ROField(this);
        }
        public ChessField(IChessFigureFactory factory) :this()
        {
            this.factory = factory;

            Init(factory);
        }
        public ChessField(IEnumerable<FigureOnBoard> figures) :this()
        {

            foreach (var item in figures)
            {
                this[item.Location] = item.Figure;
            }
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
        [NonSerialized]
        private Stack<Keeper> history;
        private IReadOnlyField readOnly;

        public IChessFigure this[Point location]
        {
            get { return field[location.X, location.Y]; }
            private set { field[location.X, location.Y] = value; }
        }
        public IChessFigure this[int x, int y]
        {
            get { return field[x, y]; }
            private set { field[x, y] = value; }
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
        public bool MakeStep(Point from, Point to)
        {
            return MakeStep(new StepInfo(from, to));
        }
        public bool MakeStep(StepInfo step)
        {

            IChessFigure attacker = this[step.From];
            IChessFigure attacked = this[step.To];

            if (attacker != null 
                && attacker.Step(step.From, step.To, readOnly) 
                && (attacked == null || attacker.Color != attacked.Color))
            {
                history.Push(
                    new Keeper(
                        step,
                        attacker, 
                        attacked
                        )
                    );

                this[step.To] = attacker;
                this[step.From] = null;

                if (attacked != null && attacked.Type == ChessFType.King)
                {
                    IsGameOver = true;
                    GameOver(attacker.Color);
                }
               
                return true;
            }

            return false;
        }
        public IReadOnlyField GetReadOnlyField()
        {
            return readOnly;
        }
        public IField Clone()
        {
            return new ChessField(GetFiguresOnBoard());
        }
        public void CancelStep()
        {
            if (history.Count > 0)
            {
                var keeper = history.Pop();

                this[keeper.Step.From] = keeper.Attacker;
                this[keeper.Step.To] = keeper.Attacked;
            }
        }

        [field:NonSerialized]
        public event Action<FColor> GameOver = (x) => { };

        private void Init(IChessFigureFactory factory)
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
    }
}
