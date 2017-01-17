using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Carcassonne.Interfaces
{
    public interface ISprite : IDrawable
    {
        Vector2 Origin { get; }
        bool HasCollided { get; }
        void LoadContent(ContentManager content, GraphicsDevice graphicsDevice, string contentName);
        void Unload();
        void Update(GameTime gameTime);
        //bool Collision(Sprite target);
    }
}
