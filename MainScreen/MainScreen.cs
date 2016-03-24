using ClientAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainScreen
{
    public class MainScreen :IMainScreen
    {
        public bool Challenge(string from)
        {
            throw new NotImplementedException();
        }

        public void Receive(string message)
        {
            throw new NotImplementedException();
        }

        public event Action RandomGame;
        public event Action<string> GameWith;
        public event Action<string> WatchForGamer;
        public event Action<string> ChangeNick;
        public event Action<string> Send;

        public System.Windows.Forms.UserControl GetScreen()
        {
            throw new NotImplementedException();
        }
    }
}
