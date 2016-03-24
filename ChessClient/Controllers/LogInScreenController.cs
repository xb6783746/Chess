using ChessClient.Interfaces;
using ChessClient.Interfaces.IControllers;
using ClientAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ChessClient.Controllers
{
    class LogInScreenController : BasicController, ISwitch, ILoginScreenController
    {
        public LogInScreenController(IMainForm form, IServerFacade facade) :base(form, facade)
        {
        }

        private AbstractLoginScreen loginScreen;

        public void Fail(string message)
        {
            loginScreen.Fail(message);
        }

        protected override void LoadScreen()
        {
            loginScreen = null;

            loginScreen.LogIn += facade.LogIn;
          
        }

        private void LogIn(IPAddress ip, int port, string nick)
        {
            facade.LogIn(ip, port, nick);
        }
    }
}
