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
        public ScreenManager(IMainForm mainForm, IServerFacade facade)
        {
            Init(mainForm, facade);

            current = screens[ScreenType.LogIn];
            current.Enable();
        }

        private Dictionary<ScreenType, ISwitch> screens;
        private ISwitch current;

        private void Init(IMainForm mainForm, IServerFacade facade)
        {
            screens = new Dictionary<ScreenType, ISwitch>();

            var game = new GameScreenController(mainForm, facade);          
            screens.Add(ScreenType.Game, game);
            this.GameController = game;

            var main = new MainScreenController(mainForm, facade);
            screens.Add(ScreenType.Main, main);
            this.MainController = main;


            var login = new LogInScreenController(mainForm, facade);
            screens.Add(ScreenType.LogIn, login);
            this.LoginController = login;

            var wait = new WaitScreenController(mainForm, facade);
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
