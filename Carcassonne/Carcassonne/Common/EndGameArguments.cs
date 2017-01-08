using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcassonne.Common
{
    public class EndGameArguments : EventArgs
    {
        private string endGame;
        private bool endGameState;
        public string Message
        {
            get
            {
                return this.endGame;
            }
        }
        public bool GameState
        {
            get
            {
                return this.endGameState;
            }
        }

        public EndGameArguments(string msg ,bool state)
        {
            this.endGameState = state;
            this.endGame = msg;
        }
    }
}
