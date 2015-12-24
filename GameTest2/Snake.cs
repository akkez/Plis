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
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Snake : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private Texture2D sprTexture;
        private Rectangle sprRectangle;
        private Vector2 sprPosition;
        private Boolean rightDirection;
        private Boolean isJumping = false;
        private int jumpTick = 0;

        private int defaultY = 480;

        public Snake(Game game, ref Texture2D givenTexture, Rectangle givenRect, Vector2 givenPosition)
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
            KeyboardState state = Keyboard.GetState();

            this.sprPosition.Y = this.defaultY;
            if (this.isJumping)
            {
                this.jumpTick++;
                if (this.jumpTick >= 50) {
                    this.isJumping = false;
                } else {
                    double x = this.jumpTick / 5.0;
                    double delta = -(x * x) + 10 * x;
                    this.sprPosition.Y = (float)(this.defaultY - delta * 4.5);
                }
            }

            if (state.IsKeyDown(Keys.W) && !this.isJumping)
            {
                this.isJumping = true;
                this.jumpTick = 0;
            }
            if (state.IsKeyDown(Keys.A) && this.sprPosition.X > 10)
            {
                this.sprPosition.X -= 5;
                rightDirection = false;
            }
            if (state.IsKeyDown(Keys.D) && this.sprPosition.X < 740)
            {
                this.sprPosition.X += 5;
                rightDirection = true;
            }
            sprRectangle.X = (((int)(this.sprPosition.X + this.sprPosition.Y) / 10) % 3) * 56 + 6;
            if (this.rightDirection)
            {
                sprRectangle.Y = 0;
            }
            else
            {
                sprRectangle.Y = 112;
            }

            
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sprBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            sprBatch.Draw(sprTexture, sprPosition, sprRectangle, Color.White, 0.0f, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.0f);
            base.Draw(gameTime);
        }

        public Rectangle getRect()
        {
            return new Rectangle((int)sprPosition.X, (int)sprPosition.Y, sprRectangle.Width, sprRectangle.Height);
        }
    }
}