namespace Carcassonne
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public static class Deck
    {

        #region Declarations

        public static List<Texture2D> textures = new List<Texture2D>();
        public static Texture2D soldier;
        public static int texturesNo;


        #endregion

        #region Initialize

        public static void Initialize(ContentManager content)
        {
            textures.Add(content.Load<Texture2D>(@"city1"));
            textures.Add(content.Load<Texture2D>(@"city11ne"));
            textures.Add(content.Load<Texture2D>(@"city11we"));
            textures.Add(content.Load<Texture2D>(@"city1rse"));
            textures.Add(content.Load<Texture2D>(@"city1rsw"));
            textures.Add(content.Load<Texture2D>(@"city1rswe"));
            textures.Add(content.Load<Texture2D>(@"city1rwe"));
            textures.Add(content.Load<Texture2D>(@"city2nw"));
            textures.Add(content.Load<Texture2D>(@"city2nws"));
            textures.Add(content.Load<Texture2D>(@"city2nwsr"));
            textures.Add(content.Load<Texture2D>(@"city2we"));
            textures.Add(content.Load<Texture2D>(@"city2wes"));
            textures.Add(content.Load<Texture2D>(@"city3"));
            textures.Add(content.Load<Texture2D>(@"city3r"));
            textures.Add(content.Load<Texture2D>(@"city3s"));
            textures.Add(content.Load<Texture2D>(@"city3sr"));
            textures.Add(content.Load<Texture2D>(@"city4"));
            textures.Add(content.Load<Texture2D>(@"cloister"));
            textures.Add(content.Load<Texture2D>(@"cloisterr"));
            textures.Add(content.Load<Texture2D>(@"road2ns"));
            textures.Add(content.Load<Texture2D>(@"road2sw"));
            textures.Add(content.Load<Texture2D>(@"road3"));
            textures.Add(content.Load<Texture2D>(@"road4"));
            soldier = (content.Load<Texture2D>(@"Soldier"));
            texturesNo = 23;
        }
        #endregion

        #region Public Methods

        public static Texture2D GetRandomTile()
        {
            Random rand = new Random();

            int x = rand.Next(0, texturesNo);

            return textures[x];
        }

        public static Texture2D GetSoldier()
        {
            return soldier;
        }

        #endregion

    }
}
