using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientAPI
{
    //public interface IWaitingScreen
    //{
    //    event Action Close;
    //}

    public abstract class AbstractWaitingScreen : UserControl
    {
        public abstract event Action Close;
    }
}
