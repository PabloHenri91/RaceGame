using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Race.src
{
    class Sprite : Entity
    {
        internal Texture2D texture;
        internal Vector2 origin;
        internal Rectangle destinationRectangle;

        public Sprite(String reference)
            : base(0, 0, 0, 0)
        {
            texture = Game1.myContentManager.Load<Texture2D>(reference);
            setSize(texture.Width, texture.Height);
            destinationRectangle.Width = texture.Width;
            destinationRectangle.Height = texture.Height;
            origin = new Vector2(width / 2f, height / 2f);
        }

        public Sprite(String reference, ContentManager contentManager)
            : base(0, 0, 0, 0)
        {
            texture = contentManager.Load<Texture2D>(reference);
            setSize(texture.Width, texture.Height);
            destinationRectangle.Width = texture.Width;
            destinationRectangle.Height = texture.Height;
            origin = new Vector2(width / 2f, height / 2f);
        }

        public Sprite(String reference, int width, int height)
            : base(0, 0, width, height)
        {
            texture = Game1.myContentManager.Load<Texture2D>(reference);
            destinationRectangle.Width = width;
            destinationRectangle.Height = height;
            origin = new Vector2(width / 2f, height / 2f);
        }

        internal void draw()
        {
            destinationRectangle.X = (int)(position.X + Game1.matrix.X);
            destinationRectangle.Y = -(int)(position.Y + Game1.matrix.Y);
            Game1.spriteBatch.Draw(texture, destinationRectangle, null, Color.White, rotation, origin, SpriteEffects.None, 0f);
        }

        internal void draw(Vector2 position)
        {
            destinationRectangle.X = (int)(position.X + Game1.matrix.X);
            destinationRectangle.Y = -(int)(position.Y + Game1.matrix.Y);
            Game1.spriteBatch.Draw(texture, destinationRectangle, null, Color.White, rotation, origin, SpriteEffects.None, 0f);
        }

        internal void draw(Vector2 position, float rotation)
        {
            destinationRectangle.X = (int)(position.X + Game1.matrix.X);
            destinationRectangle.Y = -(int)(position.Y + Game1.matrix.Y);
            Game1.spriteBatch.Draw(texture, destinationRectangle, null, Color.White, rotation, origin, SpriteEffects.None, 0f);
        }

        public void drawOnScreen()
        {
            destinationRectangle.X = (int)position.X;
            destinationRectangle.Y = (int)position.Y;
            Game1.spriteBatch.Draw(texture, destinationRectangle, null, Color.White, rotation, Vector2.Zero, SpriteEffects.None, 0f);
        }

        internal void drawOnScreen(Vector2 position)
        {
            destinationRectangle.X = (int)position.X;
            destinationRectangle.Y = (int)position.Y;
            Game1.spriteBatch.Draw(texture, destinationRectangle, null, Color.White, rotation, Vector2.Zero, SpriteEffects.None, 0f);
        }

        internal void drawOnScreen(Vector2 position, float rotation)
        {
            destinationRectangle.X = (int)position.X;
            destinationRectangle.Y = (int)position.Y;
            Game1.spriteBatch.Draw(texture, destinationRectangle, null, Color.White, rotation, origin, SpriteEffects.None, 0f);
        }
    }
}
