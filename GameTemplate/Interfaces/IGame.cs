using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTemplate.Interfaces
{
    /// <summary>
    /// Интерфейс класса игры
    /// </summary>
    /// <typeparam name="T">Интерфейс фигур на доске</typeparam>
    public interface IGame<T> where T: IFigure
    {
        /// <summary>
        /// Цвет игрока, который ходил в данный момент
        /// </summary>
        Color CurrentColor { get; }

        /// <summary>
        /// Игровое поле
        /// </summary>
        IReadOnlyField<T> Field { get; }

        /// <summary>
        /// Событие изменения состояния игры
        /// </summary>
        event Action Change;

        /// <summary>
        /// Событие завершения игры
        /// </summary>
        event Action<Color> GameOver;
    }
}
