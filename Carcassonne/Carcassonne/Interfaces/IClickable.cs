using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcassonne.Interfaces
{
    interface IClickable
    {
        int ScreenX { get; }
        int ScreenY { get; }

        uint DX { get; }

        uint DY { get; }
        bool CLickBelongToObject(int clcikedX, int clickedY);
    }
}
