using ChessClient.Enums;
using ChessClient.Facade.States;
using ChessClient.Interfaces;
using GameTemplate.ChessEnums;
using GameTemplate.Game;
using GameTemplate.Interfaces;
using Network;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClient.Controllers
{
    class ClientFacade :IClientFacade
    {
        public ClientFacade(IScreenManager manager)
        {
            repo = new Dictionary<ClientState, IFacadeState>();

            repo.Add(ClientState.Offline, new LogInFacadeState(manager));
            repo.Add(ClientState.Online, new MainFacadeState(manager));
            repo.Add(ClientState.Waiting, new WaitFacadeState(manager));
            repo.Add(ClientState.InGame, new GameFacadeState(manager));

            foreach (var tmp in repo)
            {
                tmp.Value.Switch += Switch;
            }

            current = repo[ClientState.Offline];
        }

        public ClientState State { get; private set; }

        private IFacadeState current;
        private Dictionary<ClientState, IFacadeState> repo;

        public void LoginResult(bool result, string message)
        {
            current.LoginResult(result, message);
        }
        public void Message(ChatMessage msg)
        {
            current.Message(msg);
        }
        public void Disconnected()
        {
            current.Disconnected();
        }
        public void StartGame(IReadOnlyField figures, FColor color)
        {
            current.StartGame(figures, color);
        }
        public void Challenge(string from)
        {
            current.Challenge(from);
        }
        public void UpdateField(ChessState state)
        {
            current.UpdateField(state);
        }
        public void GameOver(FColor win)
        {
            current.GameOver(win);
        }
        public void Waiting()
        {
            current.Waiting();
        }

        private void Switch(ClientState state)
        {
            current = repo[state];
            current.Enable();
          
        }

        public void GameClosed(string msg)
        {
            current.GameClosed(msg);
        }

        public void GetListOnline(string[] online)
        {
            current.GetListOnline(online);
        }
    }
}
