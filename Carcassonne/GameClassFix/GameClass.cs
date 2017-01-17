namespace GameClassFix
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.GamerServices;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Media;
    using Microsoft.Xna.Framework.Audio;
    using Carcassonne.Common;
    using Carcassonne.Constants;
    using Carcassonne;

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public sealed class GameClass : UseOtherGameClass
    {
        private static GameClass game;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        //user input detection
        private KeyboardState currentKeyboardState;
        private KeyboardState newKeyboardState;
        private MouseState currentMouseState;
        private MouseState newMouseState;
        //background Music
        private Song backgroundMusic;

        private GameClass()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.graphics.PreferredBackBufferWidth = CommonConstants.windowWidth;
            this.graphics.PreferredBackBufferHeight = CommonConstants.windowHeight;
            this.graphics.ApplyChanges();

            IsMouseVisible = true;
        }

        public static GameClass Game
        {
            get
            {
                if (game == null)
                {
                    game = new GameClass();
                }
                return game;
            }
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.currentKeyboardState = Keyboard.GetState();
            this.currentMouseState = Mouse.GetState();
            this.newKeyboardState = Keyboard.GetState();
            this.newMouseState = Mouse.GetState();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            this.spriteBatch = new SpriteBatch(GraphicsDevice);
            backgroundMusic = Content.Load<Song>("CarcassonneBackground");
            MediaPlayer.Play(backgroundMusic);
            MediaPlayer.IsRepeating = true;
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            // TODO: Add your update logic here
            this.currentKeyboardState = this.newKeyboardState;
            this.newKeyboardState = Keyboard.GetState();
            this.currentMouseState = this.newMouseState;
            this.newMouseState = Mouse.GetState();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

    }
}
