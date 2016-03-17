using GameTemplate.ChessGame.ChessInterfaces;
using GameTemplate.Game;
using GameTemplate.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientAPI
{
    public interface IGameScreen :IMessenger
    {
        event Action<StepInfo> Step;

        void StartGame();
        void UpdateField(IField<IChessFigure> f);
        void SetRender(IRender r);
        void Message(string msg);
        void GameOver(bool win);
    }
}
