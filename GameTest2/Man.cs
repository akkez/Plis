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
    public class Man : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private Texture2D sprTexture;
        private Rectangle sprRectangle;
        private Vector2 sprPosition;
        private Boolean rightDirection = true;

        private int defaultY = 470;

        public Man(MyGame game, ref Texture2D givenTexture, Rectangle givenRect, Vector2 givenPosition)
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
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (rightDirection)
            {
                this.sprPosition.X += 2;
                if (this.sprPosition.X >= 720)
                {
                    this.rightDirection = false;
                    this.sprPosition.X = 718;
                }
            }
            else
            {
                this.sprPosition.X -= 2;
                if (this.sprPosition.X <= 30)
                {
                    this.rightDirection = true;
                    this.sprPosition.X = 32;
                }
            }
            sprPosition.Y = this.defaultY;
            //sprRectangle.X = (((int)(this.sprPosition.X) / 10) % 3) * 56 + 6;
            //sprRectangle.Y = ((int)(this.sprPosition.X / 24) % 3) * 64;

            MyGame g = (MyGame)this.Game;
            if (this.getRect().Intersects(g.snakeObject.getRect())) {
                g.Finish(false);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sprBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));

            SpriteEffects mirror = this.rightDirection ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            sprBatch.Draw(sprTexture, sprPosition, sprRectangle, Color.White, 0.0f, new Vector2(0, 0), 1.0f, mirror, 0.0f);
            base.Draw(gameTime);
        }

        public Rectangle getRect()
        {
            int offset = 15;
            return new Rectangle((int)sprPosition.X + offset, (int)sprPosition.Y, sprRectangle.Width - offset, sprRectangle.Height);
        }
    }
}