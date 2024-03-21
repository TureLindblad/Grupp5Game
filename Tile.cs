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
        protected Texture2D Texture;
        public Color TileColor {  get; set; }
        public readonly int TextureResizeDimension;
        public Vector2 Origin;
        public Vector2 TexturePosition;
        public Point IndexPosition;
        public bool IsPath { get; set; }

        public Tile(int x, int y, bool isPath)
        {
            IndexPosition = new Point(x, y);
            IsPath = isPath;

            if (isPath) Texture = Assets.SandTexture;
            else Texture = Assets.GrassTexture;

            TileColor = Color.White;
            TextureResizeDimension = (int)(Texture.Width * ((float)Globals.WindowSize.X / (Texture.Width * MapScene.MapDimensions.X)));
            TextureResizeDimension = (int)(TextureResizeDimension * 1.25);

            Origin = new(TextureResizeDimension / 2, TextureResizeDimension / 2);
            TexturePosition = GetPosition(TextureResizeDimension, x, y);
        }
        protected static Vector2 GetPosition(int textureDim, int x, int y)
        {
            return new Vector2(
                x * 0.73f * textureDim + textureDim / 2,
                y * 0.98f * textureDim + (x % 2 * textureDim / 2) + textureDim / 2);
        }

        public void Update()
        {
            TileColor = Color.White;
        }

        public virtual void Draw()
        {
            Rectangle destinationRect = new Rectangle(
                (int)Math.Round(TexturePosition.X), 
                (int)Math.Round(TexturePosition.Y), 
                TextureResizeDimension, 
                TextureResizeDimension);

            Globals.SpriteBatch.Draw(Texture, destinationRect, null, TileColor, 0f, Origin, SpriteEffects.None, 1f);
        }
    }

    public class TowerTile : Tile
    {
        public List<Tuple<Vector2, bool>> AttackingPositions {  get; set; }
        public TowerTile(int x, int y, bool isPath) : base(x, y, isPath)
        {
            Texture = Assets.TowerTexture;

            int textureResizeDimension = (int)(Texture.Width * ((float)Globals.WindowSize.X / (Texture.Width * MapScene.MapDimensions.X)));
            textureResizeDimension = (int)(textureResizeDimension * 1.25);
            Vector2 texturePosition = GetPosition(textureResizeDimension, x, y);

            AttackingPositions = new List<Tuple<Vector2, bool>>();

            Vector2 v1 = new Vector2(texturePosition.X , texturePosition.Y - 20);
            Vector2 v2 = new Vector2(texturePosition.X + 10, texturePosition.Y - 50);
            Vector2 v3 = new Vector2(texturePosition.X + 150, texturePosition.Y - 70);

            AttackingPositions.Add(Tuple.Create(v1, false));
            AttackingPositions.Add(Tuple.Create(v2, false));
            AttackingPositions.Add(Tuple.Create(v3, false));

        }

        public override void Draw()
        {
            Rectangle destinationRect = new Rectangle(
                (int)Math.Round(TexturePosition.X),
                (int)Math.Round(TexturePosition.Y),
                TextureResizeDimension,
                TextureResizeDimension);

            Globals.SpriteBatch.Draw(Texture, destinationRect, null, TileColor, 0f, Origin, SpriteEffects.None, 1f);
        }
    }
}
