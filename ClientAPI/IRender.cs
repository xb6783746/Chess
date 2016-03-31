using GameTemplate.Game;
using GameTemplate.Interfaces;
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
        void UpdateField(Bitmap bitmap, IReadOnlyList<FigureOnBoard> field);
        void DrawCells(IReadOnlyList<FigureOnBoard> field, List<Point> cells);
    }
}
