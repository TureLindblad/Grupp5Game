using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Grupp5Game
{
    public class Enemy
    {
        private Texture2D Texture;
        public int Size { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        private int Speed;
        private List<Tile> CompletedTileList;
        public Enemy()
        {
            Texture = Assets.EnemyGoblinTexture;

            Position = new Vector2(0, 0);
            Size = 50;
            Velocity = new Vector2(0, 0);
            Speed = 2;
            CompletedTileList = new List<Tile>();
        }

        public void Update(MapScene mapScene)
        {
            float minDistance = float.MaxValue;
            Tile[,] tiles = mapScene.MapGrid.Tiles;
            Tile closestTile = null;

            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < mapScene.MapGrid.Tiles.GetLength(1); y++)
                {
                    float distance = Vector2.Distance(Position, tiles[x, y].Position);
                    if (tiles[x, y].IsPath && distance < minDistance && !CompletedTileList.Contains(tiles[x, y]))
                    {
                        minDistance = distance;
                        closestTile = tiles[x, y];
                    }
                }
            }

            if (closestTile != null)
            {
                Vector2 direction = Vector2.Normalize(closestTile.Position - Position);

                Velocity = direction * Speed;

                if (minDistance < Speed)
                {
                    CompletedTileList.Add(closestTile);
                }

                Position += Velocity;
            }

            if (CompletedTileList.Count == mapScene.MapGrid.NumberOfPathTiles) mapScene.EnemyList.Remove(this);
        }

        public void Draw(MapScene mapScene)
        {
            Globals.SpriteBatch.Draw(
                Texture, 
                new Rectangle(
                    (int)Position.X - Size / 2 + 5, //5 hjälper men vet inte varför den behövs 
                    (int)Position.Y - Size / 2 - 15, //-15 för att rita lite över mitten
                    Size, 
                    Size), 
                Color.White);
        }
    }
}
