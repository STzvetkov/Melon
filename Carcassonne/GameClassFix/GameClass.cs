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
    using Carcassonne.Menu;
    using System.Collections.Generic;
    using System.Threading;

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
        //tiles
        private Sprite startTile;
        //menu
        private IList<MenuItem> menuOptions;
        private Texture2D menuBackground;
        private Texture2D aboutBackground;
        private Rectangle backgroundRect;
        private int menuIndex;
        private GameState gameState;
        private SpriteFont menuFont;
        private SpriteFont gameFont;



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
            this.Services.AddService(typeof(GraphicsDeviceManager), this.graphics);
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
            this.Services.AddService(typeof(SpriteBatch), this.spriteBatch);
            //single click capture
            this.currentKeyboardState = Keyboard.GetState();
            this.currentMouseState = Mouse.GetState();
            this.newKeyboardState = Keyboard.GetState();
            this.newMouseState = Mouse.GetState();
            //deck initialization
            Deck.Initialize(Content);
            //starting tile
            this.startTile = new Sprite(new Vector2((CommonConstants.windowWidth / 2) - CommonConstants.tileSize / 2, (CommonConstants.windowHeight / 2) - CommonConstants.tileSize / 2));

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

            this.menuBackground = Content.Load<Texture2D>("background");
            this.aboutBackground = this.Content.Load<Texture2D>("aboutBackground");
            this.menuFont = this.Content.Load<SpriteFont>("ArialMenu");
            this.gameFont = this.Content.Load<SpriteFont>("Arial");
            this.backgroundRect = new Rectangle(0, 0, CommonConstants.windowWidth, CommonConstants.windowHeight);
            this.menuOptions = new List<MenuItem>
            {
              new MenuItem("New game", new Vector2(CommonConstants.windowWidth / 2, 270), Color.Crimson, this.Content),
              new MenuItem("About", new Vector2(CommonConstants.windowWidth / 2, 320), Color.Crimson, this.Content),
              new MenuItem("Exit", new Vector2(CommonConstants.windowWidth / 2, 370), Color.Crimson, this.Content)
             };

            this.menuIndex = 1;
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

            this.currentKeyboardState = this.newKeyboardState;
            this.newKeyboardState = Keyboard.GetState();
            this.currentMouseState = this.newMouseState;
            this.newMouseState = Mouse.GetState();

            switch (gameState)
            {
                case GameState.Menu:
                    if (this.newKeyboardState.IsKeyDown(Keys.Enter) && this.currentKeyboardState.IsKeyUp(Keys.Enter))
                    {
                        this.gameState = (GameState)menuIndex;
                    }
                    if (this.newKeyboardState.IsKeyDown(Keys.Down) && this.currentKeyboardState.IsKeyUp(Keys.Down) && menuIndex <= 2)
                    {
                        menuIndex += 1;
                    }
                    if (this.newKeyboardState.IsKeyDown(Keys.Up) && this.currentKeyboardState.IsKeyUp(Keys.Up) && menuIndex >= 2)
                    {
                        menuIndex -= 1;
                    }
                    break;
                case GameState.NewGame:

                    if (this.newKeyboardState.IsKeyDown(Keys.M) && this.currentKeyboardState.IsKeyUp(Keys.M))
                    {
                        this.gameState = GameState.Menu;
                    }
                    //TODO: update ingame logic
                    break;
                case GameState.About:

                    if (this.newKeyboardState.IsKeyDown(Keys.M) && this.currentKeyboardState.IsKeyUp(Keys.M))
                    {
                        this.gameState = GameState.Menu;
                    }

                    break;
                case GameState.Exit:
                    Exit();
                    break;
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            switch (this.gameState)
            {
                case GameState.Menu:
                    this.spriteBatch.Draw(this.menuBackground, this.backgroundRect, Color.White);
                    for (int i = 0; i < menuOptions.Count; i += 1)
                    {
                        if (i == menuIndex - 1)
                        {
                            this.menuOptions[i].UpdateColor(Color.Black);
                        }
                        else
                        {
                            this.menuOptions[i].UpdateColor(Color.Crimson);
                        }
                        this.menuOptions[i].Draw(this.spriteBatch);
                    }
                    break;
                case GameState.About:
                    this.spriteBatch.Draw(this.aboutBackground, this.backgroundRect, Color.White);
                    this.spriteBatch.DrawString(this.menuFont, "About", new Vector2(CommonConstants.aboutXOffset, CommonConstants.aboutYOffset), Color.White);
                    this.spriteBatch.DrawString(this.gameFont, CommonConstants.aboutMessage, new Vector2(CommonConstants.aboutXOffset, (CommonConstants.aboutXOffset + CommonConstants.aboutTextYOffset * 2)), Color.White);
                    this.spriteBatch.DrawString(this.gameFont, CommonConstants.aboutMessageNote, new Vector2(CommonConstants.aboutXOffset, (CommonConstants.aboutXOffset + CommonConstants.aboutTextYOffset / 2 * 7)), Color.White);
                    this.spriteBatch.DrawString(this.gameFont, CommonConstants.aboutMessageUI, new Vector2(CommonConstants.aboutXOffset, (CommonConstants.aboutXOffset + CommonConstants.aboutTextYOffset / 2 * 9)), Color.White);
                    break;
                case GameState.Exit:
                    this.spriteBatch.Draw(this.menuBackground, this.backgroundRect, Color.White);
                    break;
                default:
                    //TODO: implement ingame objects here
                    break;
            }
                    spriteBatch.End();
                    base.Draw(gameTime);
            }

        }
    }
}
