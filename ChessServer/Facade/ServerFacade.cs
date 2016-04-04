using ChessServer.Interfaces;
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
        public ServerFacade(IChatManager chatManager, IClientManager clientManager, IGameManager gameManager, IClientFacade clientFacade)
        {
            this.chatManager = chatManager;
            this.gameManager = gameManager;
            this.clientManager = clientManager;
            this.clientFacade = clientFacade;
        }

        private IChatManager chatManager;
        private IGameManager gameManager;
        private IClientManager clientManager;
        private IClientFacade clientFacade;

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
            var gamer = clientManager.GetClient(id);
            if (gamer != null)
            {
                gameManager.RandomGame(gamer);
            }
        }

        public void GetOnline(int id)
        {
            clientFacade.SendOnlineList(clientManager.GetOnlineClient(id), id);
        }
    }
}
