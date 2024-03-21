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
        public Rectangle Bounds { get { 
                return new Rectangle((int)Position.X - Size / 2, (int)Position.Y - Size / 2 , Size , Size); 
            } 
        }
        public Rectangle BoundsWithNextMovement
        {
            get
            {
                int xMod = (int)(Velocity.X * Speed);
                int yMod = (int)(Velocity.Y * Speed);
                return new Rectangle(Bounds.X + xMod, Bounds.Y + yMod, Bounds.Width + xMod, Bounds.Height + yMod);
            }
        }
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
            float minDistancePath = float.MaxValue;
            float minDistanceTower = float.MaxValue;
            Tile[,] tiles = mapScene.MapGrid.Tiles;
            Tile closestTile = null;

            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < mapScene.MapGrid.Tiles.GetLength(1); y++)
                {
                    float distance = Vector2.Distance(Position, tiles[x, y].TexturePosition);
                    if (tiles[x, y].IsPath && distance < minDistancePath && !CompletedTileList.Contains(tiles[x, y]))
                    {
                        minDistancePath = distance;
                        closestTile = tiles[x, y];
                    }
                    else if (tiles[x, y] is TowerTile && distance < minDistanceTower)
                    {
                        minDistanceTower = distance;
                        closestTile = tiles[x, y];
                    }
                }
            }

            if (closestTile != null)
            {
                Vector2 direction = Vector2.Normalize(closestTile.TexturePosition - Position);

                Velocity = direction * Speed;

                if (minDistancePath < Speed)
                {
                    CompletedTileList.Add(closestTile);
                }

                if (closestTile is TowerTile && minDistanceTower < Speed)
                {
                    tiles[closestTile.IndexPosition.X, closestTile.IndexPosition.Y] = 
                        new Tile(closestTile.IndexPosition.X, closestTile.IndexPosition.Y, false);
                }

                if (!Collision.CheckCollision(this, mapScene.EnemyList))
                {
                    Velocity *= -1;
                    
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