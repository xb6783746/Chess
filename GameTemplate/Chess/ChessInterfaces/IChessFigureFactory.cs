using GameTemplate.ChessGame.ChessEnums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTemplate.ChessGame.ChessInterfaces
{
    /// <summary>
    /// Интерфейс фабрики шахматных фигур
    /// </summary>
    public interface IChessFigureFactory
    {
        IChessFigure GetFigure(ChessFType type, Color color);
    }
}
