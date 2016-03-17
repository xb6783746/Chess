using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientAPI
{
    public interface IMainScreen :IMessenger
    {
        event Action RandomGame;
        event Action<string> GameWith;
        event Action<string> WatchForGamer;
        event Action<string> ChangeNick;

        bool Challenge();
    }
}
