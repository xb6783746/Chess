using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ClientAPI
{
    public interface ILoginScreen
    {
        event Action<IPAddress, int> LogIn;
    }
}
