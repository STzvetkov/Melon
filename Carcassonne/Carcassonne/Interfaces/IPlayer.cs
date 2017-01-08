using Carcassonne.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcassonne.Interfaces
{
   public  interface IPlayer
    {
        void AddPawn(Pawn p);
        void RemovePawn(Pawn p);
    }
}
