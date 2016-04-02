using ChessServer.Interfaces;
using Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessServer.Chat
{
    //class ChatManager :IChatManager
    //{
    //    public ChatManager(IClientManager clientManager, IGameManager gameManager)
    //    {
    //        this.clientManager = clientManager;
    //        this.gameManager = gameManager;

    //        clientManager.Connected += NewClient;
    //        clientManager.Disconnected += Disconnected;
    //        gameManager.GameStart += GameStart;
    //        gameManager.GameOver += GameOver;

    //        mainRoom = new ChatRoom();
    //        rooms = new List<ChatRoom>();
    //        clients = new List<IClient>();

    //        rooms.Add(mainRoom);
    //    }

    //    private IClientManager clientManager;
    //    private IGameManager gameManager;
    //    private ChatRoom mainRoom;
    //    private List<ChatRoom> rooms;

    //    private List<IClient> clients;

    //    public void Message(ChatMessage message, int id)
    //    {
    //        if (message.Type == ChatMessageType.Private)
    //        {

    //            var clientTo = clients.FirstOrDefault((x) => x.Nick == message.To);
    //            var clientFrom = clients.FirstOrDefault((x) => x.Id == id);               

    //            if (clientFrom != null)
    //            {
    //                message.From = clientFrom.Nick;

    //                if (clientTo == null)
    //                {
    //                    clientFrom.Send(
    //                        new ChatMessage(
    //                            "System", 
    //                            clientFrom.Nick, 
    //                            ChatMessageType.System,
    //                            "Игрока с таким ником не существует"));
    //                }
    //                else
    //                {
    //                    clientTo.Send(message);
    //                }
    //            }


    //            return;
    //        }

    //        var room = rooms.FirstOrDefault((x) => x.Contains(id));

    //        if (room != null)
    //        {
    //            room.Send(message, id);
    //        }
    //    }

    //    private void NewClient(IClient client)
    //    {
    //        mainRoom.Add(client);
    //        clients.Add(client);
    //    }
    //    private void Disconnected(IClient client)
    //    {
    //        var room = rooms.FirstOrDefault((x) => x.Contains(client.Id));

    //        if (room != null)
    //        {
    //            room.Remove(client);
    //        }
    //    }
    //    private void GameStart(int id)
    //    {

    //    }
    //    private void GameOver(int id)
    //    {

    //    }
    //}
    class ChatManager : IChatManager
    {
        class ClientInChat
        {
            public ClientInChat(IClient client, int roomId)
            {
                this.Client = client;
                this.RoomId = roomId;
            }

            public IClient Client { get; set; }
            public int RoomId { get; set; }
        }

        public ChatManager(IClientManager clientManager, IGameManager gameManager, IClientFacade clientFacade)
        {
            this.clientManager = clientManager;
            this.gameManager = gameManager;
            this.clientFacade = clientFacade;

            clientManager.Connected += NewClient;
            clientManager.Disconnected += Disconnected;
            gameManager.GameStart += GameStart;
            gameManager.GameOver += GameOver;

            rooms = new List<ClientInChat>();

        }

        private int mainRoomId = -1;

        private IClientManager clientManager;
        private IGameManager gameManager;
        private IClientFacade clientFacade;
        private List<ClientInChat> rooms;
        private object lck = new object();

        public void Message(ChatMessage message, int id)
        {
            var sender = rooms.FirstOrDefault((x) => x.Client.Id == id);
            if (sender == null)
            {
                return;
            }

            if (message.Type == ChatMessageType.Private)
            {
                PrivateMessage(sender, message, id);
            }
            else
            {
                PublicMessage(sender, message, id);
            }
        }

        private void PrivateMessage(ClientInChat sender, ChatMessage message, int id)
        {
            var to = rooms.FirstOrDefault((x) => x.Client.Nick == message.To);
            message.From = sender.Client.Nick;

            if (to == null)
            {
                var sysMess = new ChatMessage(
                    "System",
                    sender.Client.Nick,
                    ChatMessageType.System,
                    "Игрок с таким ником не существует"
                    );

                clientFacade.Message(sysMess, sender.Client.Id);
            }
            else
            {
                clientFacade.Message(message, to.Client.Id);
            }
        }
        private void PublicMessage(ClientInChat sender, ChatMessage message, int id)
        {
            message.From = sender.Client.Nick;

            var clients = rooms.Where((x) => x.RoomId == sender.RoomId && x.Client.Id != id);

            foreach (var client in clients)
            {
                clientFacade.Message(message, client.Client.Id);
            }

        }
        private void NewClient(IClient client)
        {
            lock (lck)
            {
                rooms.Add(
                    new ClientInChat(client, mainRoomId)
                    );

                var clients = rooms.Where((x) => x.RoomId == mainRoomId);

                ChatMessage m = new ChatMessage(
                    "System",
                    "",
                    ChatMessageType.Info,
                    client.Nick + " вошел в игру"
                    );

                foreach (var cl in clients)
                {
                    clientFacade.Message(m, cl.Client.Id);
                }
            }
        }
        private void Disconnected(IClient client)
        {
            lock (lck)
            {
                rooms.RemoveAll((x) => x.Client == client);

                ChatMessage m = new ChatMessage(
                    "System",
                    "",
                    ChatMessageType.Info,
                    client.Nick + " вышел из игры"
                    );

                var clients = rooms.Where((x) => x.RoomId == mainRoomId);
                foreach (var cl in clients)
                {
                    clientFacade.Message(m, cl.Client.Id);
                }
            }
        }

        private void GameStart(int id)
        {
            var watchers = gameManager.Watchers(id);
            var clients = rooms.Where((x) => watchers.Contains(x.Client));

            foreach (var client in clients)
            {
                client.RoomId = id;
            }
        }
        private void GameOver(int id)
        {
            var clients = rooms.Where((x) => x.RoomId == id);

            foreach (var client in clients)
            {
                client.RoomId = mainRoomId;
            }
        }
    }
}
