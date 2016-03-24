﻿using ChessClient.Interfaces;
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
        public LogInScreenController(IMainForm form, IServerFacade facade) :base(form, facade)
        {
        }

        private ILoginScreen loginScreen;

        public void Fail(string message)
        {
            loginScreen.Fail(message);
        }

        protected override void LoadScreen()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "/Screens";
            string[] files = Directory.GetFiles(path, "*.dll");
            foreach (var item in files)
            {
                var assembly = Assembly.LoadFile(item);

                var screen = assembly.GetTypes().FirstOrDefault((x) => x.GetInterfaces().Contains(typeof(ILoginScreen)));

                if (screen != null)
                {
                    loginScreen = Activator.CreateInstance(screen) as ILoginScreen;
                    this.screen = loginScreen.GetScreen();
                }
            }

            loginScreen.LogIn += facade.LogIn;
          
        }

        private void LogIn(IPAddress ip, int port, string nick)
        {
            facade.LogIn(ip, port, nick);
        }
    }
}
