using GameTemplate.ChessEnums;
using GameTemplate.ChessGame.ChessEnums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTemplate.Interfaces
{
    /// <summary>
    /// Интерфейс фабрики шахматных фигур
    /// </summary>
    public interface IChessFigureFactory
    {
        IChessFigure GetFigure(ChessFType type, FColor color);
    }
}
