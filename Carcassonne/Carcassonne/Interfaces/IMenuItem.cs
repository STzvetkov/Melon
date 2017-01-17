namespace Carcassonne.Interfaces
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public interface IMenuItem
    {
        Color Color { get; }
        void Draw(SpriteBatch spriteBatch);
        void UpdateColor(Color color);
    }
}
