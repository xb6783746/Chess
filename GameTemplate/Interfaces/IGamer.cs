using GameTemplate.ChessEnums;
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
    public interface IGamer
    {
        /// <summary>
        /// Цвет игрока
        /// </summary>
        FColor Color { get; }

        /// <summary>
        /// Инициализирует экземпляр игрока
        /// </summary>
        /// <param name="game">Экземпляр игры</param>
        /// <param name="color">Цвет игрока</param>
        void Init(IGame game, FColor color);
        /// <summary>
        /// Сделать ход
        /// </summary>
        /// <returns>StepInfo, содержащий информацию о сделанном ходе</returns>
        StepInfo MakeStep();
    }
}
