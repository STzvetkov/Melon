using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Carcassonne.Interfaces
{
    public interface IDrawable
    {
        Rectangle Rectangle { get; }
        Texture2D Texture { get; }
        Vector2 Position { get; }

        void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}
