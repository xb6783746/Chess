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
    /// Интерфейс класса игрока
    /// </summary>
    /// <typeparam name="T">Интерфейс фигур на доске</typeparam>
    public interface IGamer<T> where T : IFigure
    {
        /// <summary>
        /// Инициализирует экземпляр игрока
        /// </summary>
        /// <param name="game">Экземпляр игры</param>
        /// <param name="color">Цвет игрока</param>
        void Init(IGame<T> game, Color color);

        /// <summary>
        /// Сделать ход
        /// </summary>
        /// <returns>StepInfo, содержащий информацию о сделанном ходе</returns>
        StepInfo MakeStep();

        /// <summary>
        /// Цвет игрока
        /// </summary>
        Color Color { get; }
    }
}
