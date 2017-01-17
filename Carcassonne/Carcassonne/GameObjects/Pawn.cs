using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcassonne.GameObjects
{
    public abstract class Pawn:MapObjects
    {
        public abstract void Move(byte x, byte y);

    }
}
