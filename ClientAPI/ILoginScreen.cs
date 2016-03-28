using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientAPI
{
    public interface ILoginScreen : IScreen
    {
        event Action<string> LogIn;

        void Fail(string message);
    }

    //public abstract class AbstractLoginScreen : UserControl, IMessenger
    //{
    //    public abstract event Action<IPAddress, int, string> LogIn;

    //    public abstract void Fail(string message);

    //    public abstract event Action<string> Send;

    //    public abstract void Receive(string message);
    //}
}
