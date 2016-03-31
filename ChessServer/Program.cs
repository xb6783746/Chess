using ChessServer.Chat;
using ChessServer.Facade;
using ChessServer.IdManager;
using ChessServer.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var idManager = new IDManager();
            var clientFacade = new ClientFacade();
            var clientManager = new ClientManager(clientFacade);
            var server = new SocketServer(clientManager, idManager);
            var chatManager = new ChatManager(clientManager);
            var gameManager = new GameManager(clientFacade);
            var serverFacade = new ServerFacade(chatManager, clientManager, gameManager);

            clientFacade.Init(server, serverFacade);

            server.Start();
           
        }
    }
}
