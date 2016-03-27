using ChessServer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessServer.Chat
{
    class ChatManager :IChatManager
    {
        public ChatManager(IClientManager clientManager)
        {
            this.clientManager = clientManager;

            clientManager.Connected += NewClient;
            clientManager.Disconnected += Disconnected;

            mainRoom = new ChatRoom();
            rooms = new List<ChatRoom>();
            clients = new List<IClient>();

            rooms.Add(mainRoom);
        }

        private IClientManager clientManager;

        private ChatRoom mainRoom;
        private List<ChatRoom> rooms;

        private List<IClient> clients;

        public void Message(string message, int id)
        {
            if (message[0] == '/')
            {
                string[] tmp = message.Substring(1).Split(' ');

                string nick = tmp[0];
                string mesg = message.Substring(2 + nick.Length);

                var clientTo = clients.FirstOrDefault((x) => x.Nick == nick);
                var clientFrom = clients.FirstOrDefault((x) => x.Id == id);

                if (clientTo != null && clientFrom != null)
                {
                    clientTo.Send("/" + clientFrom.Nick + " " + mesg);
                }

                return;
            }

            var room = rooms.FirstOrDefault((x) => x.Contains(id));

            if (room != null)
            {
                room.Send(message, id);
            }
        }

        private void NewClient(IClient client)
        {
            mainRoom.Add(client);
            clients.Add(client);
        }
        private void Disconnected(IClient client)
        {
            var room = rooms.FirstOrDefault((x) => x.Contains(client.Id));

            if (room != null)
            {
                room.Remove(client);
            }
        }
    }
}
