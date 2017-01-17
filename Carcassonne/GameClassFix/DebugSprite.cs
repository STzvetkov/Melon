namespace GameClassFix
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// The idea is to have idea where the on the screen is the "debugged" sprite
    /// </summary>
    public class DebugSprite : Sprite
    {
        private Color rectangleColor;
        private Texture2D rectangleTexture;
        private Color originalColor;

        public DebugSprite(Vector2 position, Color rectangleColor, float speed = 0, float angle = 0, float rotationSpeed = 0, Rectangle? bounds = null) : base(position, speed, angle,rotationSpeed, bounds)
        {
            this.rectangleColor = rectangleColor;
            this.originalColor = Color.Black * 0.1f;
        }

        protected override void OnContentLoaded(ContentManager content, GraphicsDevice graphicsDevice)
        {
            Color[] colors = new Color[base.Texture.Width * base.Texture.Height];
            colors[0] = this.rectangleColor; // top left
            colors[base.Texture.Width - 1] = this.rectangleColor; // top right
            colors[(base.Texture.Width * base.Texture.Height) - Texture.Width] = this.rectangleColor; // bottom left
            colors[(base.Texture.Width * base.Texture.Height) - 1] = this.rectangleColor; //bottom right

            this.rectangleTexture = new Texture2D(graphicsDevice, base.Texture.Width, base.Texture.Height);
            this.rectangleTexture.SetData(colors);

            base.OnContentLoaded(content, graphicsDevice);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(this.rectangleTexture, null, base.Rectangle, null, null, 0, null, Color.White);
            //rotation
            spriteBatch.Draw(Texture, Position, null, null, Vector2.Zero, 0, null, this.originalColor);

            base.Draw(spriteBatch, gameTime);
        }
    }
}
