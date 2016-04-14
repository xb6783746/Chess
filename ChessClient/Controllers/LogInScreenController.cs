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
        public LogInScreenController(IMainForm form, IServer facade)
            : base(form, facade)
        {
            LoadScreen();
        }

        private ILoginScreen loginScreen;
        private const string config = "config.txt";

        public void Fail(string message)
        {
            loginScreen.Fail(message);
        }

        protected override void LoadScreen()
        {
            var screen = GetType(screenDir, typeof(ILoginScreen));

            loginScreen = Activator.CreateInstance(screen) as ILoginScreen;
            this.screen = loginScreen;

            loginScreen.LogIn += LogIn;

        }

        private void LogIn(string nick)
        {
            IPAddress ip;
            int port;

            try
            {
                LoadConfig(out ip, out port);
            }
            catch
            {
                loginScreen.Fail("Ошибка при загрузке конфигурационного файла");
                return;
            }

            facade.LogIn(ip, port, nick);

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
