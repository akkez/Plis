using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameTest2
{
    class CloseButton : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private Texture2D sprTexture;
        private Rectangle sprRectangle;
        private Vector2 sprPosition;
        private ButtonState beforeState;

        private Random random = new Random();

        public CloseButton(MyGame game, ref Texture2D givenTexture, Rectangle givenRect, Vector2 givenPosition)
                : base(game)
        {
            this.sprTexture = givenTexture;
            this.sprRectangle = givenRect;
            this.sprPosition = givenPosition;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            MouseState state = Mouse.GetState();
            if (state.LeftButton == ButtonState.Pressed && state.LeftButton != this.beforeState)
            {
                Rectangle bounds = new Rectangle((int)this.sprPosition.X, (int)this.sprPosition.Y, this.sprRectangle.Width, this.sprRectangle.Height);
                if (bounds.Contains(state.X, state.Y))
                {
                    MyGame g = (MyGame)this.Game;
                    g.isRunning = !g.isRunning;
                }
            }
            this.beforeState = state.LeftButton;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sprBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            sprRectangle.Y = ((MyGame)this.Game).isRunning ? 100 : 0;
            sprBatch.Draw(sprTexture, sprPosition, sprRectangle, Color.White);

            
            base.Draw(gameTime);
        }
    }
}
