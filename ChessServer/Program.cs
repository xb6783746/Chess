using ChessServer.Chat;
using ChessServer.Facade;
using ChessServer.IdManager;
using ChessServer.Managers;
using GameTemplate.ChessGame.ChessField;
using GameTemplate.ChessGame.ChessFigures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
            var gameManager = new GameManager(clientFacade);
            var chatManager = new ChatManager(clientManager, gameManager, clientFacade);
            var serverFacade = new ServerFacade(chatManager, clientManager, gameManager);

            clientFacade.Init(server, serverFacade);

            Task.Run(() => server.Start());

            while (Console.ReadLine().ToLower() != "stop") ;
           
        }
    }
}
