using ChessClient.Enums;
using ChessClient.Facade.States;
using ChessClient.Interfaces;
using GameTemplate.ChessGame.ChessInterfaces;
using GameTemplate.Game;
using GameTemplate.Interfaces;
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
        public void Message(string msg)
        {
            current.Message(msg);
        }
        public void Disconnected()
        {
            current.Disconnected();
        }
        public void StartGame(Color color)
        {
            current.StartGame(color);
        }
        public void Challenge(string from)
        {
            current.Challenge(from);
        }
        public void UpdateField(IField<IChessFigure> field, StepInfo step)
        {
            current.UpdateField(field, step);
        }
        public void GameOver(bool win)
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
    }
}
