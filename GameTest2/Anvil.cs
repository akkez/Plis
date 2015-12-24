using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameTest2
{
    class Anvil : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private Texture2D sprTexture;
        private Rectangle sprRectangle;
        private Vector2 sprPosition;
        private bool isVisible = true;
        private bool orient;

        public Anvil(MyGame game, ref Texture2D givenTexture, Rectangle givenRect, Vector2 givenPosition, bool givenOrient)
            : base(game)
        {
            this.sprTexture = givenTexture;
            this.sprRectangle = givenRect;
            this.sprPosition = givenPosition;
            this.orient = givenOrient;
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
            if (!isVisible)
            {
                return;
            }

            Rectangle myRect = this.getRect();
            MyGame g = (MyGame)this.Game;

            if (this.sprPosition.Y >= 500)
            {
                this.Gone();
            }
            else
            {
                this.sprPosition.Y += 4;
            }

            if (myRect.Intersects(g.snakeObject.getRect()))
            {
                g.Finish(false);
            }
            if (myRect.Intersects(g.manObject.getRect()))
            {
                this.Gone();
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
            SpriteEffects eff = SpriteEffects.None;
            if (this.orient == true)
            {
                eff = SpriteEffects.FlipHorizontally;
            }
            sprBatch.Draw(sprTexture, sprPosition, sprRectangle, Color.White, 0.0f, new Vector2(), 1.0f, eff, 0);

            base.Draw(gameTime);
        }

        public Rectangle getRect()
        {
            int offset = 8;
            return new Rectangle((int)sprPosition.X + offset, (int)sprPosition.Y + offset, sprRectangle.Width - offset, sprRectangle.Height - offset);
        }
    }
}
