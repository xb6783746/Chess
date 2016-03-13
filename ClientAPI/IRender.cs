using GameTemplate.Game;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientAPI
{
    public interface IRender
    {
        void UpdateField(Bitmap bitmap, StepInfo step);
    }
}
