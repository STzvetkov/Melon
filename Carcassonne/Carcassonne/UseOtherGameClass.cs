using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Carcassonne.Common;
using Carcassonne.Constants;

namespace Carcassonne
{
    public class UseOtherGameClass : Game
    {
        //If you can think of other workaround for circular-dependency hit me - S.A.
        // will implement Singleton through the monogame project. Start the project from there.

        private static UseOtherGameClass game;
        private Map map;
        public virtual Map Map { get { return this.map; } set { this.map = value; } }

        internal static UseOtherGameClass Game
        {
            get
            {
                if (game == null)
                {
                    game = new UseOtherGameClass();
                }
                return game;
            }
        }


    }
}