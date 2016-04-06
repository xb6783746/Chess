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
        void Init(int wh, int ht);
        Bitmap UpdateField(IReadOnlyList<FigureOnBoard> field);
        Bitmap UpdateField(IReadOnlyList<FigureOnBoard> field, StepInfo step);
        Bitmap DrawCells(IReadOnlyField field, Point from, List<Point> cells);
    }
}
