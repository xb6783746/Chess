using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTemplate.ChessEnums
{
    [Serializable]
    public enum ChessFType
    {
        King, //Король
        Queen, //Ферзь
        Rook, //Ладья
        Bishop, //Слон
        Knight, //Конь
        Pawn //Пешка
    }
}
