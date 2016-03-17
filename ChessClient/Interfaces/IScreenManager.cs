using ChessClient.Enums;
using ChessClient.Interfaces.IControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClient.Interfaces
{
    interface IScreenManager
    {
        void Switch(ScreenType screen);

        IGameScreenController GameController {get;}
        ILoginScreenController LoginController { get; }
        IMainScreenController MainController { get; }
    }
}
