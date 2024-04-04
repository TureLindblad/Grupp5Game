using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp5Game
{
    public abstract class Tile
    {
        protected Texture2D Texture;
        public Color TileColor {  get; set; }
        public readonly int TextureResizeDimension;
        public Vector2 Origin;
        public Vector2 TexturePosition;
        public Point IndexPosition;

        public Tile(int x, int y)
        {
            Texture = Assets.SandTexture;
            IndexPosition = new Point(x, y);

            int rightUIWidth = 120;
            int topMinusOffset = 30;
            int leftMinusOffset = 10;

            TileColor = Color.White;
            TextureResizeDimension 
                = (int)(Texture.Width * ((float)(Globals.WindowSize.X - rightUIWidth) / (Texture.Width * Globals.MapDimensions.X)));
            TextureResizeDimension = (int)(TextureResizeDimension * 1.33);

            Origin = new(TextureResizeDimension / 2, TextureResizeDimension / 2);
            TexturePosition = GetPosition(TextureResizeDimension, x, y);
            TexturePosition = new Vector2(TexturePosition.X - leftMinusOffset, TexturePosition.Y - topMinusOffset);
        }
        protected static Vector2 GetPosition(int textureDim, int x, int y)
        {
            return new Vector2(
                x * 0.73f * textureDim + textureDim / 2,
                y * 0.99f * textureDim + (x % 2 * textureDim / 2) + textureDim / 2);
        }

        public virtual void Update(GameTime gameTime)
        {
            TileColor = Color.White;
        }

        public void Draw()
        {
            Rectangle destinationRect = new Rectangle(
                (int)Math.Round(TexturePosition.X), 
                (int)Math.Round(TexturePosition.Y), 
                TextureResizeDimension, 
                TextureResizeDimension);

            Globals.SpriteBatch.Draw(Texture, destinationRect, null, TileColor, 0f, Origin, SpriteEffects.None, 1f);
        }
    }

    public class NexusTile : Tile
    {
        public NexusTile(int x, int y, bool isNexusCenter) : base(x, y) 
        {
            if (isNexusCenter) Texture = Assets.NexusTexture;
            else Texture = Assets.NexusTextureOuter;
        }
    }

    public class SandTile : Tile
    {
        public SandTile(int x, int y) : base(x, y) 
        {
            Texture = Assets.SandTexture;
        }
    }

    public class MountainTile : Tile
    {
        public MountainTile(int x, int y) : base(x, y)
        {
            Texture = Assets.SandMountainTexture;
        }
    }

    public class PathTile : Tile
    {
        public PathTile(int x, int y) : base(x, y)
        {
            Texture = Assets.StoneTexture;
        }
    }

    public class SandMountainTile : Tile
    {
        public SandMountainTile(int x, int y) : base(x, y)
        {
            Texture = Assets.SandMountainTexture;
        }
    }

    public class BedouinTile : Tile
    {
        public BedouinTile(int x, int y) : base(x, y)
        {
            Texture = Assets.BedouinTexture;
        }
    }

    public class SandRockTile : Tile
    {
        public SandRockTile(int x, int y) : base(x, y)
        {
            Texture = Assets.SandRockTexture;
        }
    }

    public class OasisTile : Tile
    {
        public OasisTile(int x, int y) : base(x, y)
        {
            Texture = Assets.OasisTexture;
        }
    }
}
