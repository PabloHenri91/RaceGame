using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Race.src
{
    class Tileset : Sprite
    {
        private int tileWidth, tileHeight, tilesPerColumn;
        internal int tilesCount, tilesPerRow;
        private Rectangle source;
        private Vector2 scale;

        public Tileset(String reference, int tileWidth, int tileHeight)
            : base(reference, tileWidth, tileHeight)
        {
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
            tilesPerRow = texture.Width / tileWidth;
            tilesPerColumn = texture.Height / tileHeight;
            tilesCount = tilesPerRow * tilesPerColumn;
            source = new Rectangle(0, 0, tileWidth, tileHeight);
            origin = Vector2.Zero;
            destinationRectangle.Width = tileWidth;
            destinationRectangle.Height = tileHeight;
        }

        public Tileset(String reference, int tileWidth, int tileHeight, Vector2 scale)
            : base(reference, tileWidth, tileHeight)
        {
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
            tilesPerRow = texture.Width / tileWidth;
            tilesPerColumn = texture.Height / tileHeight;
            tilesCount = tilesPerRow * tilesPerColumn;
            source = new Rectangle(0, 0, tileWidth, tileHeight);
            origin = Vector2.Zero;
            this.scale = scale;
            destinationRectangle.Width = (int)(tileWidth * scale.X);
            destinationRectangle.Height = (int)(tileHeight * scale.Y);
        }

        internal void drawTile(int x, int y, int texCoordx, int texCoordy)
        {
            source.X = texCoordx * tileWidth;
            source.Y = texCoordy * tileHeight;
            destinationRectangle.X = (int)(x + Game1.matrix.X);
            destinationRectangle.Y = -(int)(y + Game1.matrix.Y);
            Game1.spriteBatch.Draw(texture, destinationRectangle, source, Color.White, rotation, origin, SpriteEffects.None, 0f);
        }

        internal void drawTile(int x, int y, int texCoordx, int texCoordy, Color color)
        {
            source.X = texCoordx * tileWidth;
            source.Y = texCoordy * tileHeight;
            destinationRectangle.X = (int)(x + Game1.matrix.X);
            destinationRectangle.Y = -(int)(y + Game1.matrix.Y);
            Game1.spriteBatch.Draw(texture, destinationRectangle, source, color, rotation, origin, SpriteEffects.None, 0f);
        }

        internal void drawTile(Vector2 position, int texCoordx, int texCoordy)
        {
            source.X = texCoordx * tileWidth;
            source.Y = texCoordy * tileHeight;
            destinationRectangle.X = (int)(position.X + Game1.matrix.X);
            destinationRectangle.Y = -(int)(position.Y + Game1.matrix.Y);
            Game1.spriteBatch.Draw(texture, destinationRectangle, source, Color.White, rotation, origin, SpriteEffects.None, 0f);
        }
    }
}
