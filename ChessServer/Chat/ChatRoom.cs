using ChessServer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessServer.Chat
{
    class ChatRoom
    {
        public ChatRoom()
        {
            clients = new List<IClient>();
        }

        private List<IClient> clients;
        private object lck = new object();

        public void Add(IClient client)
        {
            lock (lck)
            {
                clients.Add(client);
            }
        }
        public void Remove(IClient client)
        {
            lock (lck)
            {
                clients.Remove(client);
            }
        }

        public List<IClient> GetClients()
        {
            return clients;
        }
        public bool Contains(int id)
        {
            return clients.Exists((x) => x.Id == id);
        }

        public void Send(string mesg, int id)
        {
            if (Contains(id))
            {

                var tmp = clients.Where((x) => x.Id != id);

                mesg = clients.First((x) => x.Id == id).Nick + ": " + mesg;
                foreach (var client in tmp)
                {
                    client.Send(mesg);
                }
            }
           
        }
    }
}
