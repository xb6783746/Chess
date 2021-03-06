﻿using GameTemplate.Game;
using Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ChessClient.Interfaces
{
    public interface IServerFacade
    {
        void LogIn(IPAddress ip, int port, string nick);
        void ChangeNick(string nick);
        void StartRandomGame();
        void StartGameWith(string gamer);
        void WatchFor(string gamer);
        void MakeStep(StepInfo step);
        void SendMessage(ChatMessage msg);
        void Surrender();
        void Accept();
        void Disconnect();
        void GetOnline();
    }
}
