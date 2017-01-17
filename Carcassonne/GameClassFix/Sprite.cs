namespace GameClassFix
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Carcassonne.Interfaces;

    public class Sprite : ISprite
    {
        private Color color;
        private Vector2 velocity;
        private Vector2 position;
        private Rectangle rectangle;
        private Texture2D texture;
        // rectangle binding
        private Rectangle? bounds;
        //rotation
        private float angle;
        private float rotationSpeed;
        private Vector2 origin;

        public Sprite(Vector2 position, float speed = 0f, float angle = 0f, float rotationSpeed = 0, Rectangle? bounds = null)
        {
            this.position = position;
            this.velocity = new Vector2((float)(speed * Math.Cos(angle)), (float)(speed * Math.Sin(angle)));
            this.texture = null;
            this.color = Color.White;

            this.bounds = bounds;

            this.angle = angle;
            this.rotationSpeed = rotationSpeed;
            this.origin = Vector2.Zero;
        }

        public Texture2D Texture => this.texture;
        public Vector2 Position => this.position;
        public Rectangle Rectangle => this.rectangle;
        public Vector2 Origin => this.origin;

        public bool HasCollided { get; private set; }

        public void LoadContent(ContentManager content, GraphicsDevice graphicsDevice, string contentName)
        {
            this.texture = content.Load<Texture2D>(contentName);
            OnContentLoaded(content, graphicsDevice);
        }

        protected virtual void OnContentLoaded(ContentManager content, GraphicsDevice graphicsDevice)
        {
            this.origin = new Vector2(this.texture.Width / 2.0f, this.texture.Height / 2.0f);

            UpdateRectangle();
        }

        private void UpdateRectangle()
        {
            Vector2 topLeft = this.position - this.origin;

            this.rectangle = new Rectangle((int)topLeft.X, (int)topLeft.Y, this.texture.Width, this.texture.Height);
        }

        public virtual void Unload()
        {
            this.texture.Dispose();
        }

        public void Update(GameTime gameTime)
        {
            this.position += this.velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            UpdateRotation(gameTime);
            UpdateRectangle();
            CheckBounds();
        }

        private void UpdateRotation(GameTime gameTime)
        {
            this.angle += (float)(this.rotationSpeed * gameTime.ElapsedGameTime.TotalSeconds);
            if (this.angle < 0)
            {
                this.angle = MathHelper.TwoPi - Math.Abs(this.angle);
            }
            else if (this.angle > MathHelper.TwoPi)
            {
                this.angle = this.angle = MathHelper.TwoPi;
            }
        }

        private void CheckBounds()
        {
            if (this.bounds == null) { return; }

            Vector2 change = Vector2.Zero;
            if (this.rectangle.Left <= this.bounds.Value.X)
            {
                change.X = this.bounds.Value.X - this.rectangle.Left;
            }
            else if (this.rectangle.Right >= this.bounds.Value.Right)
            {
                change.X = this.bounds.Value.Right - this.rectangle.Right;
            }

            if (this.rectangle.Top <= this.bounds.Value.Y)
            {
                change.Y = this.bounds.Value.Y - this.rectangle.Top;
            }
            else if (this.rectangle.Bottom >= this.bounds.Value.Bottom)
            {
                change.Y = this.bounds.Value.Bottom - this.rectangle.Bottom;
            }

            if (change == Vector2.Zero) { return; }

            this.position = new Vector2((int)this.position.X + change.X, (int)this.position.Y + change.Y);
            UpdateRectangle();
        }

        public bool Collision(Sprite target)
        {
            bool intersects = this.rectangle.Intersects(target.rectangle);
            //both get the value
            this.HasCollided = intersects;
            target.HasCollided = intersects;

            return intersects;
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //spriteBatch.Draw(this.texture, this.position, this.color);

            //rotation
            spriteBatch.Draw(this.texture, this.position, null, null, this.origin, this.angle, null, this.color);

        }
    }
}
