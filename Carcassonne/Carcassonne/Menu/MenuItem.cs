namespace Carcassonne.Menu
{
    using Interfaces;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public class MenuItem : IMenuItem
    {
        private readonly string title;
        private readonly SpriteFont font;
        private readonly Vector2 vector;
        private Color color;

        public MenuItem(string title, Vector2 vector, Color color, ContentManager content)
        {

            this.title = title;
            this.vector = vector;
            this.Color = color;
            this.font = content.Load<SpriteFont>("ArialMenu");

        }

        public Color Color
        {
            get
            {
                return this.Color;
            }
            private set
            {
                this.color = value;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 textSize = this.font.MeasureString(this.title);
            Vector2 textCenter = new Vector2(this.vector.X, this.vector.Y);

            spriteBatch.DrawString(this.font, this.title, textCenter - (textSize / 2), this.color);
        }

        public void UpdateColor(Color color)
        {
            this.Color = color;
        }
    }
}
