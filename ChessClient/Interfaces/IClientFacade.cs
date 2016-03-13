using GameTemplate.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClient.Interfaces
{
    public interface IClientFacade
    {
        void Connect(bool isConnected, string nick);

        void SetGamerList(List<string> gamers);

        void StartGame();
        void Update(StepInfo step, bool turn);
       // void Update(IField<IChessFigure> field);
        void GameOver(bool win);        
        void Surrendered();

        void StartWatch();

        void NewMessage(string message);
    }
}
