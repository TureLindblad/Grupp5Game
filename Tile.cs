using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp5Game
{
    public class Tile
    {
        public readonly Texture2D Texture;
        public Color TileColor {  get; set; }
        public readonly int TextureResizeDimension;
        public Vector2 Origin;
        public Vector2 Position;
        public bool IsPath { get; set; }

        public Tile(int x, int y, bool isPath)
        {
            IsPath = isPath;

            if (isPath) Texture = Assets.SandTexture;
            else Texture = Assets.GrassTexture;

            TileColor = Color.White;
            TextureResizeDimension = (int)(Texture.Width * ((float)Game1.WindowSize.X / (Texture.Width * Game1.MapDimensions.X)));
            TextureResizeDimension = (int)(TextureResizeDimension * 1.25);

            Origin = new(TextureResizeDimension / 2, TextureResizeDimension / 2);
            Position = GetPosition(TextureResizeDimension, x, y);
        }
        private static Vector2 GetPosition(int textureDim, int x, int y)
        {
            return new Vector2(
                x * 0.73f * textureDim + textureDim / 2,
                y * 0.98f * textureDim + (x % 2 * textureDim / 2) + textureDim / 2);
        }

        public void Update()
        {
            TileColor = Color.White;
        }

        public void Draw(Game1 game)
        {
            Rectangle destinationRect = new Rectangle(
                (int)Math.Round(Position.X), 
                (int)Math.Round(Position.Y), 
                TextureResizeDimension, 
                TextureResizeDimension);

            game._spriteBatch.Draw(Texture, destinationRect, null, TileColor, 0f, Origin, SpriteEffects.None, 1f);
        }
    }
}
