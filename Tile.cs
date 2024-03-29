﻿using Microsoft.Xna.Framework;
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
            Texture = Assets.GrassTexture;

            IndexPosition = new Point(x, y);

            TileColor = Color.White;
            TextureResizeDimension = (int)(Texture.Width * ((float)Globals.WindowSize.X / (Texture.Width * Globals.MapDimensions.X)));
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
        public NexusTile(int x, int y) : base(x, y) 
        {
            Texture = Assets.NexusTexture;
        }
    }

    public class TerrainTile : Tile
    {
        public TerrainTile(int x, int y) : base(x, y) 
        {
            Texture = Assets.GrassTexture;
        }
    }

    public class PathTile : Tile
    {
        public PathTile(int x, int y) : base(x, y)
        {
            Texture = Assets.SandTexture;
        }
    }

    public class TowerTile : Tile
    {
        public List<Tuple<Vector2, bool>> AttackingPositions {  get; set; }
        public TowerTile(int x, int y) : base(x, y)
        {
            Texture = Assets.TowerTexture;
            AttackingPositions = new List<Tuple<Vector2, bool>>();

            Vector2 v1 = new Vector2(TexturePosition.X + 30, TexturePosition.Y + 30);
            Vector2 v2 = new Vector2(TexturePosition.X, TexturePosition.Y - 30);
            Vector2 v3 = new Vector2(TexturePosition.X - 30, TexturePosition.Y + 30);

            AttackingPositions.Add(Tuple.Create(v1, false));
            AttackingPositions.Add(Tuple.Create(v2, false));
            AttackingPositions.Add(Tuple.Create(v3, false));
        }
    }
}
