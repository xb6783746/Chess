using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientAPI
{
    public interface IMainScreen : IMessenger, IScreen
    {
        event Action RandomGame;
        event Action<string> GameWith;
        event Action<string> WatchForGamer;
        event Action<string> ChangeNick;
        event Action GetOnline;

        bool Challenge(string from);
        void SetOnlineList(string[] online);

        string Nick { get; set; }
    }

    //public abstract class AbstractMainScreen : UserControl, IMessenger
    //{
    //    public abstract event Action RandomGame;
    //    public abstract event Action<string> GameWith;
    //    public abstract event Action<string> WatchForGamer;
    //    public abstract event Action<string> ChangeNick;

    //    public abstract bool Challenge(string from);

    //    public abstract event Action<string> Send;

    //    public abstract void Receive(string message);
    //}

}
