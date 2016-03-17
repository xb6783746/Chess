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

        public IGameScreenController GameController {get;}
        public ILoginScreenController LoginController { get; }
        public IMainScreenController MainController { get; }
    }
}
