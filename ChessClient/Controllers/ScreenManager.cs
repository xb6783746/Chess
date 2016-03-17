using ChessClient.Enums;
using ChessClient.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClient.Controllers
{
    class ScreenManager :IScreenManager
    {
        public ScreenManager()
        {
            Init();
        }

        private Dictionary<ScreenType, ISwitch> screens;
        private ISwitch current;

        private void Init()
        {
            screens = new Dictionary<ScreenType, ISwitch>();

            var game = new GameScreenController();          
            screens.Add(ScreenType.Game, game);
            this.GameController = game;

            var main = new MainScreenController();
            screens.Add(ScreenType.Main, main);
            this.MainController = main;


            var login = new LogInScreenController();
            screens.Add(ScreenType.LogIn, new LogInScreenController());
            this.LoginController = login;

            var wait = new WaitScreenController();
            screens.Add(ScreenType.Wait, wait);
        }

        public void Switch(Enums.ScreenType screen)
        {
            current.Disable();

            current = this.screens[screen];

            current.Enable();
        }

        public Interfaces.IControllers.IGameScreenController GameController
        {
            get;
            private set;
        }
        public Interfaces.IControllers.ILoginScreenController LoginController
        {
            get;
            private set;
        }
        public Interfaces.IControllers.IMainScreenController MainController
        {
            get;
            private set;
        }
    }
}
