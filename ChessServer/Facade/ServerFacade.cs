﻿using ChessServer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessServer.Facade
{
    class ServerFacade :IServerFacade
    {
        public ServerFacade(IChatManager chatManager, IClientManager clientManager)
        {
            this.chatManager = chatManager;
            this.clientManager = clientManager;
        }

        private IChatManager chatManager;
        private IClientManager clientManager;

        public void Message(string message, int id)
        {
            chatManager.Message(message, id);
        }
    }
}