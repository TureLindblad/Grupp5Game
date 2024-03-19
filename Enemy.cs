using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Grupp5Game
{
    public abstract class Enemy
    {
        public Rectangle Bounds { get { return new Rectangle((int)Position.X - 10 , (int)Position.Y - 10 , 20 , 20 ); } }
        private Texture2D Texture;
        public int Health { get; set; }
        public int PhysArmor { get; set; }
        public int MagicArmor { get; set; }
        public int AttackDamage { get; set; }
        public int AttackSpeed { get; set; }
        public int Size { get; set; }
        public int Speed { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        private List<Tile> CompletedTileList;
        public Enemy(Texture2D texture)
        {
            Texture = texture;

            Position = new Vector2(0, 0);

            Velocity = new Vector2(0, 0);

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
                    float distance = Vector2.Distance(Position, tiles[x, y].TexturePosition);
                    if (tiles[x, y].IsPath || tiles[x, y] is TowerTile && distance < minDistance && !CompletedTileList.Contains(tiles[x, y]))
                    {
                        minDistance = distance;
                        closestTile = tiles[x, y];
                    }
                }
            }

            if (closestTile != null)
            {
                Vector2 direction = Vector2.Normalize(closestTile.TexturePosition - Position);

                Velocity = direction * Speed;

                if (minDistance < Speed)
                {
                    CompletedTileList.Add(closestTile);
                }

                if (closestTile is TowerTile && minDistance < Speed)
                {
                    tiles[closestTile.IndexPosition.X, closestTile.IndexPosition.Y] = 
                        new Tile(closestTile.IndexPosition.X, closestTile.IndexPosition.Y, false);
                }

                if (!Collision.CheckCollision(this, mapScene.EnemyList))
                {
                    Position += Velocity;
                }
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