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

            rooms.Add(mainRoom);
        }

        private IClientManager clientManager;

        private ChatRoom mainRoom;
        private List<ChatRoom> rooms;

        public void Message(string message, int id)
        {
            var room = rooms.FirstOrDefault((x) => x.Contains(id));

            if (room != null)
            {
                room.Send(message, id);
            }
        }

        private void NewClient(IClient client)
        {
            mainRoom.Add(client);
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
