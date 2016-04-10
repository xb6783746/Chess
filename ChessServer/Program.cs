using ChessServer.Chat;
using ChessServer.Facade;
using ChessServer.IdManager;
using ChessServer.Managers;
using ChessServer.Network;
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
            var parser = new Parser();
            var server = new SocketServer(idManager, parser);
            ClientFacade.Init(server);
            var clientFacade = ClientFacade.Instance;
            
            var clientManager = new ClientManager(clientFacade, server);
            var gameManager = new GameManager(clientFacade, clientManager);
            var chatManager = new ChatManager(clientManager, gameManager, clientFacade);
            var serverFacade = new ServerFacade(chatManager, clientManager, gameManager, clientFacade);

            parser.SetFacade(serverFacade);
            //clientFacade.Init(server);

            Task.Run(() => server.Start());

            while (Console.ReadLine().ToLower() != "stop") ;
           
        }
    }
}
