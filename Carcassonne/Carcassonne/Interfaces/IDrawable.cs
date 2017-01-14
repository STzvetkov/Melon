using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Carcassonne.Interfaces
{
    interface IDrawable
    {
        Rectangle Rectangle { get; }

        Texture2D Texture { get; }

        void Draw(SpriteBatch spriteBatch);
    }
}
