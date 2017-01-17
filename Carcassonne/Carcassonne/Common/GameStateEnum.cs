using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcassonne.Common
{
    public enum GameState
    {
        Menu = 0,
        NewGame,
        About,
        UpdateResult,
        EndResult,
        Exit,
        Win,
        PlayerOnTurn,
    }
}
