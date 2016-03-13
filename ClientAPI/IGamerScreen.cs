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
        event Action Lose;

        void UpdateField(StepInfo info);
        void SetRender(IRender render);

        void GameOver(bool win, string msg);
    }
}
