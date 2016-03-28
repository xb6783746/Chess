using ChessClient.Interfaces;
using ChessClient.Interfaces.IControllers;
using ClientAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ChessClient.Controllers
{
    class LogInScreenController : BasicController, ISwitch, ILoginScreenController
    {
        public LogInScreenController(IMainForm form, IServerFacade facade)
            : base(form, facade)
        {
        }

        private ILoginScreen loginScreen;
        private string config = "config.txt";

        public void Fail(string message)
        {
            loginScreen.Fail(message);
        }

        protected override void LoadScreen()
        {
            var screen = GetScreenType("/Screens", typeof(ILoginScreen));

            loginScreen = Activator.CreateInstance(screen) as ILoginScreen;
            this.screen = loginScreen.GetScreen();

            loginScreen.LogIn += LogIn;

        }

        private void LogIn(string nick)
        {
            try
            {
                IPAddress ip;
                int port;

                LoadConfig(out ip, out port);

                facade.LogIn(ip, port, nick);
            }
            catch
            {
                loginScreen.Fail("Ошибка при загрузке конфигурационного файла");
            }

        }

        private void LoadConfig(out IPAddress ip, out int port)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + config;
            using (StreamReader read = new StreamReader(path))
            {
                ip = IPAddress.Parse(read.ReadLine());
                port = int.Parse(read.ReadLine());
            }
        }
    }
}
