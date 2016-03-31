using GameTemplate.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTemplate.Game
{
    /// <summary>
    /// Простейшая реализация интерфейса IGame
    /// </summary>
    /// <typeparam name="T">Интерфейс фигур на доске</typeparam>
    [Serializable]
    public class SimpleGame :IGame
    {
        public SimpleGame(IGamer first, IGamer second, IField field)
        {
            gamerQueue = new Queue<IGamer>();
            ok = true;

            gamerQueue.Enqueue(first);
            gamerQueue.Enqueue(second);

            first.Init(this, Color.Black);
            second.Init(this, Color.White);

            this.field = field;

            field.GameOver += GameOverHandler;

            Task.Run(() => StartGame());

        }

        /// <summary>
        /// Очередь из игроков
        /// </summary>
        protected Queue<IGamer> gamerQueue;
        /// <summary>
        /// Игровое поле
        /// </summary>
        protected IField field;
        /// <summary>
        /// Маркер ошибок
        /// </summary>
        protected bool ok;

        /// <summary>
        /// Цвет игрока, который ходит в данный момент
        /// </summary>
        public Color CurrentColor
        {
            get;
            private set;
        }
        /// <summary>
        /// Игровое поле
        /// </summary>
        public IReadOnlyField Field
        {
            get { return field.GetReadOnlyField(); }
        }

        /// <summary>
        /// Запуск игры
        /// </summary>
        protected virtual void StartGame()
        {
            IGamer current;
            while (!field.IsGameOver && ok)
            {
                current = gamerQueue.Dequeue();

                NextStep(current);

                gamerQueue.Enqueue(current);
            }

        }
        /// <summary>
        /// Обработка хода игрока
        /// </summary>
        /// <param name="gamer">Игрок, ход которого обрабатывается</param>
        protected virtual void NextStep(IGamer gamer)
        {
            CurrentColor = gamer.Color;
            StepInfo step;
            do
            {
                step = gamer.MakeStep();
            } 
            while (!field.MakeStep(step));

            if (Change != null)
            {
                Change();
            }
        }
        /// <summary>
        /// Обработчик события завершения игры (из игрового поля)
        /// </summary>
        /// <param name="color"></param>
        protected virtual void GameOverHandler(Color color)
        {
            GameOver(color);
        }

        /// <summary>
        /// Событие изменения состояния игры
        /// </summary>
        public event Action Change;
        /// <summary>
        /// Событие завершения игры
        /// </summary>
        public event Action<Color> GameOver = (x) => { };
     
    }
}
