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

namespace ChessServer.Interfaces
{
    interface IClientFacade
    {
        void Message(ChatMessage msg, int id);
        void LoginResult(bool result, string message, int id);

        void Wait(int id);
        void StartGame(IReadOnlyField field, FColor color, int id);
        void Update(ChessState state, int id);
        void GameOver(FColor win, int id);
        void SendOnlineList(string[] online, int id);
        void GameClosed(string message, int id);
    }
}
