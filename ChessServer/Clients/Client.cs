﻿using ChessServer.Interfaces;
using GameTemplate.Interfaces;
using Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessServer.Clients
{
    class Client :IClient, IDisposable
    {
        public Client(int id, IClientFacade facade)
        {
            this.Id = id;
            this.clientFacade = facade;

            gamer = new RemoteGamer(id);
        }

        private IClientFacade clientFacade;
        private RemoteGamer gamer;

        public int Id
        {
            get;
            private set;
        }
        public string Nick
        {
            get;
            set;
        }
        public IGamer Gamer { get { return gamer; } }

        public void Step(GameTemplate.Game.StepInfo step)
        {
            gamer.Step(step);
        }

        public void Dispose()
        {
            if (gamer != null)
            {
                gamer.Dispose();
            }
        }
    }
}
