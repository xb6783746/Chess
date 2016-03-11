using GameTemplate.Game;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTemplate.Interfaces
{
    /// <summary>
    /// Интерфейс класса игрового поля
    /// </summary>
    /// <typeparam name="T">Интерфейс фигур на доске</typeparam>
    public interface IField<T> where T : IFigure
    {
        T this[Point location] { get; }

        /// <summary>
        /// Получить все фигуры, находящиеся на доске
        /// </summary>
        List<FigureOnBoard<T>> GetFiguresOnBoard(); 

        /// <summary>
        /// Получить все вышедшие из игры фигуры
        /// </summary>
        List<FigureOnBoard<T>> GetDiedFigures();

        /// <summary>
        /// Сделать ход из клетки с координатами from в клетку с координатами to
        /// </summary>
        /// <param name="from">Координата ходящей фигуры</param>
        /// <param name="to">Координата, куда ходит фигура с координатами from</param>
        void MakeStep(Point from, Point to);

        /// <summary>
        /// Сделать ход согласно заданной StepInfo
        /// </summary>
        /// <param name="step">Информация о ходе</param>
        void MakeStep(StepInfo step);

        /// <summary>
        /// Завершена ли игра
        /// </summary>
        bool IsGameOver { get; }

        /// <summary>
        /// Событие завершения игры
        /// </summary>
        event Action<Color> GameOver;
    }
}
