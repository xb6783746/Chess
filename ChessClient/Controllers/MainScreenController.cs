using ChessClient.Interfaces;
using ChessClient.Interfaces.IControllers;
using ClientAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ChessClient.Controllers
{
    class MainScreenController : BasicController, ISwitch, IMainScreenController
    {
        public MainScreenController(IMainForm form, IServerFacade facade) :base(form, facade)
        {
        }

        public void Challenge(string from)
        {
            mainScreen.Challenge(from);
        }
        public void Message(string msg)
        {
            mainScreen.Receive(msg);
        }


        private IMainScreen mainScreen;

        protected override void LoadScreen()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "/Screens";
            string[] files = Directory.GetFiles(path, "*.dll");
            foreach (var item in files)
            {
                var assembly = Assembly.LoadFile(item);

                var screen = assembly.GetTypes().FirstOrDefault((x) => x.GetInterfaces().Contains(typeof(IMainScreen)));

                if (screen != null)
                {
                    mainScreen = Activator.CreateInstance(screen) as IMainScreen;
                    this.screen = mainScreen.GetScreen();
                }
            }


            mainScreen.ChangeNick += ChangeNick;
            mainScreen.GameWith += GameWith;
            mainScreen.RandomGame += RandomGame;
            mainScreen.Send += Send;
            mainScreen.WatchForGamer += Watch;
        }

        private void ChangeNick(string nick)
        {
            facade.ChangeNick(nick);
        }
        private void GameWith(string gamer)
        {
            facade.StartGameWith(gamer);
        }
        private void RandomGame()
        {
            facade.StartRandomGame();
        }
        private void Send(string message)
        {
            facade.SendMessage(message);
        }
        private void Watch(string gamer)
        {
            facade.WatchFor(gamer);
        }

    }
}
