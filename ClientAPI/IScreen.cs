using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientAPI
{
    public interface IScreen
    {
        UserControl GetScreen();

        void Enable();
        void Disable();
    }
}
