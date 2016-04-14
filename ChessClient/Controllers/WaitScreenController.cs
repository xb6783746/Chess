﻿using ChessClient.Interfaces;
using ClientAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClient.Controllers
{
    class WaitScreenController : BasicController, ISwitch
    {
        public WaitScreenController(IMainForm mainForm, IServer facade) :base(mainForm, facade)
        {
            LoadScreen();
        }

        private IWaitingScreen waitScreen;

        protected override void LoadScreen()
        {
            var type = this.GetType(screenDir, typeof(IWaitingScreen));

            waitScreen = Activator.CreateInstance(type) as IWaitingScreen;

            this.screen = waitScreen;
        }
    }
}
