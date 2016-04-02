using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessServer.Interfaces;
using ChessServer.IdManager;
using ChessServer.Clients;

namespace ChessServer.Managers
{
    class ClientManager : IClientManager
    {

        public ClientManager(IClientFacade clientFacade)
        {
            clients = new Dictionary<int, IClient>();
            withoutNick = new Dictionary<int, IClient>();

            this.clientFacade = clientFacade;
        }

        private Dictionary<int, IClient> withoutNick;
        private Dictionary<int, IClient> clients;
        private IClientFacade clientFacade;
        private object lck = new object();

        public void Registration(int id)
        {
            IClient client = new Client(id, clientFacade);
            lock (lck)
            {
                withoutNick.Add(id, client);
            }
        }
        public void Disconnect(int id)
        {           
            Disconnected(clients[id]);

            lock (lck)
            {
                clients.Remove(id);
            }
        }
        public void ChangeNick(string nick, int id)
        {
            lock (lck)
            {
                if (withoutNick.ContainsKey(id))
                {
                    clients.Add(id, withoutNick[id]);
                    withoutNick.Remove(id);

                    clients[id].Nick = nick;

                    clients[id].LoginResult(true, nick);
                    Connected(clients[id]);                   
                }

                clients[id].Nick = nick;
            }
        }
        public IClient GetClient(int id)
        {
            if (clients.ContainsKey(id))
            {
                return clients[id];
            }

            return null;
        }

        public event Action<IClient> Connected = (x) => { };
        public event Action<IClient> Disconnected = (x) => { };
    }
}
