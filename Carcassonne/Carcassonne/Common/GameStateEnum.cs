using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcassonne.Common
{
    public enum GameStateEnum
    {
        Menu = 0,
        NewGame,
        UpdateResult,
        EndResult,
        Exit,
        Win,
        PlayerOnTurn
    }
}
