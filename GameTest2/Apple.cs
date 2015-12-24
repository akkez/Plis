using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameTest2
{
    class Apple : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private Texture2D sprTexture;
        private Rectangle sprRectangle;
        private Vector2 sprPosition;
        private bool isVisible = true;
        private int lieTick = 0;
        private bool isPicked = false;

        public Apple(MyGame game, ref Texture2D givenTexture, Rectangle givenRect, Vector2 givenPosition)
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
            if (!isVisible) {
                return;
            }

            Rectangle myRect = this.getRect();
            MyGame g = (MyGame)this.Game;

            if (!this.isPicked)
            {
                if (this.sprPosition.Y >= 500)
                {
                    this.lieTick++;
                }
                else
                {
                    this.sprPosition.Y += 4;
                }
                if (this.lieTick > 75)
                {
                    this.Gone();
                }

                if (myRect.Intersects(g.snakeObject.getRect()))
                {
                    g.score++;
                    this.isPicked = true;
                    this.lieTick = 0;
                }
                if (myRect.Intersects(g.manObject.getRect()))
                {
                    this.Gone();
                }
            }

            if (this.isPicked)
            {
                this.lieTick++;
                if (this.lieTick > 50)
                {
                    this.Gone();
                }
            }
            
            base.Update(gameTime);
        }

        private void Gone()
        {
            this.Game.Components.Remove(this);
            this.isVisible = false;
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sprBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            sprRectangle.Y = this.isPicked ? 26 : 0;
            sprBatch.Draw(sprTexture, sprPosition, sprRectangle, Color.White);

            base.Draw(gameTime);
        }

        public Rectangle getRect()
        {
            return new Rectangle((int)sprPosition.X, (int)sprPosition.Y, sprRectangle.Width, sprRectangle.Height);
        }
    }
}
