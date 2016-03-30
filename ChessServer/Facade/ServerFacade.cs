﻿using ChessServer.Interfaces;
using Network;
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
        private IGameManager gameManager;
        private IClientManager clientManager;

        public void Message(ChatMessage message, int id)
        {
            chatManager.Message(message, id);
        }


        public void ChangeNick(string nick, int id)
        {
            clientManager.ChangeNick(nick, id);
        }


        public void NewStep(GameTemplate.Game.StepInfo step, int id)
        {
            var client = clientManager.GetClient(id);

            if (client != null)
            {
                client.Step(step);
            }
        }

        public void RandomGame(int id)
        {
            gameManager.RandomGame(id);
        }
    }
}
