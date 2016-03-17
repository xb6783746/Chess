using GameTemplate.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientAPI
{
    public interface IGamerScreen :IMessenger
    {
        event Action<StepInfo> Step;

        void StartGame();

        void UpdateField(IChessField f);

        void SetRender(IRender r);

        void Message(string msg);

        void GameOver(bool win);
    }
}
