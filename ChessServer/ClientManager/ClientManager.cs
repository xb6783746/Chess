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
            this.clientFacade = clientFacade;
        }

        private Dictionary<int, IClient> clients;
        private IClientFacade clientFacade;
        private object lck = new object();

        public void Registration(int id)
        {
            IClient client = new Client(id, clientFacade);
            lock (lck)
            {
                clients.Add(id, client);
            }

            client.LoginResult(true, "");

            Connected(client);
        }

        public void Disconnect(int id)
        {           
            Disconnected(clients[id]);

            lock (lck)
            {
                clients.Remove(id);
            }
        }

        public event Action<IClient> Connected = (x) => { };

        public event Action<IClient> Disconnected = (x) => { };


        public void ChangeNick(string nick, int id)
        {
            clients[id].Nick = nick;
        }
    }
}
