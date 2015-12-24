using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace GameTest2
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MyGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Snake snakeObject;
        public Man manObject;
        CloseButton pauseBtn;

        public int tick = 0;
        public int score = 0;
        public int maxTime = 25 * 1000;
        private int elapsedTime = 0;
        public bool isRunning = true;
        public bool isOver = false;
        public bool gameResult = false;

        Random random = new Random(Guid.NewGuid().GetHashCode());

        SpriteFont scoreFont;
        Texture2D textureAnvil;
        Texture2D textureApple;
        Texture2D textureMan;
        Texture2D texturePause;
        Texture2D textureSky;
        Texture2D textureSnake;
        
        public MyGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            this.IsMouseVisible = true;
            this.Window.Title = "SNAKE: THE GAME";
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(typeof(SpriteBatch), spriteBatch);

            this.textureAnvil = Content.Load<Texture2D>("anvil");
            this.textureApple = Content.Load<Texture2D>("apple");
            this.textureMan = Content.Load<Texture2D>("man");
            this.texturePause = Content.Load<Texture2D>("pause");
            this.textureSky = Content.Load<Texture2D>("sky");
            this.textureSnake = Content.Load<Texture2D>("snake");

            this.scoreFont = Content.Load<SpriteFont>("Arial");
            CreateComponents();
        }

        private void CreateComponents()
        {
            snakeObject = new Snake(this, ref this.textureSnake, new Rectangle(0, 0, 56, 56),
                new Vector2(550, 480));
            Components.Add(snakeObject);

            pauseBtn = new CloseButton(this, ref this.texturePause, new Rectangle(0, 0, 100, 100),
                new Vector2(800 - 100, 0));
            Components.Add(pauseBtn);

            manObject = new Man(this, ref this.textureMan, new Rectangle(0, 0, 40, 64), new Vector2(40, 64));
            Components.Add(manObject);
        }   

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            if (this.isOver)
            {
                return;
            }
            if (!this.isRunning)
            {
                this.pauseBtn.Update(gameTime);
                return;
            }
            tick++;

            if (tick % 15 == 0)
            {
                SpawnObjectives();
            }

            elapsedTime += gameTime.ElapsedGameTime.Milliseconds;
            if (elapsedTime > this.maxTime)
            {
                this.Finish(true);
            }
            base.Update(gameTime);
        }

        private void SpawnObjectives()
        {
            int pos = this.random.Next(50, 750);
            if (this.random.Next(0, 99) > 20)
            {
                Apple obj = new Apple(this, ref this.textureApple, new Rectangle(0, 0, 27, 26), new Vector2(pos, 70));
                Components.Add(obj);
            }
            else
            {
                Anvil obj = new Anvil(this, ref this.textureAnvil, new Rectangle(0, 0, 64, 35), new Vector2(pos, 60));
                Components.Add(obj);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(this.textureSky, new Rectangle(0, 0, 800, 600), Color.Wheat);

            string status = "";
            Color textColor;
            if (isOver) {
                status = this.gameResult ? "You win!" : "You lose!";
                status = status + " Score: " + this.score.ToString();
                textColor = this.gameResult ? Color.GreenYellow : Color.Red;
            } else {
                double expired = (this.maxTime - this.elapsedTime) / 1000.0;
                status = "Score: " + this.score.ToString() + "                  (" + string.Format("{0:F1}", expired) + "s)";
                textColor = Color.Yellow;
            }
            int x = GraphicsDevice.Viewport.Width / 2 - (int)scoreFont.MeasureString(status).X / 2;
            spriteBatch.DrawString(scoreFont, status, new Vector2(x, 30), textColor);

            base.Draw(gameTime);
            spriteBatch.End();
        }

        public void Finish(bool result)
        {
            this.gameResult = result;
            this.isOver = true;
            this.isRunning = false;

            Components.Remove(this.pauseBtn);
        }
    }
}
