using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace Grupp5Game
{
    public abstract class Enemy
    {
        public Rectangle Bounds { 
            get 
            { 
                return new Rectangle((int)Position.X - Size / 2, (int)Position.Y - Size / 2, Size, Size); 
            } 
        }
        
        private Texture2D Texture;
        public int PhysArmor { get; set; }
        public int MagicArmor { get; set; }
        public int AttackDamage { get; set; }
        public int AttackSpeed { get; set; }
        public int Size { get; set; }
        public int Speed { get; set; }
        public int GoldValue { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        private List<Tile> CompletedTileList;
        public bool IsAttacking { get; set; }
        public bool MarkedForDeletion { get; set; } = false;
        public HealthBar HealthBar { get; protected set; }

        public Enemy(Texture2D texture)
        {
            Texture = texture;

            Position = new Vector2(0, (int)(Globals.WindowSize.Y / 2.0f));

            Velocity = Vector2.Zero;

            CompletedTileList = new List<Tile>();
        }

        public void Update(PlayMapScene mapScene)
        {
            HealthBar.Update();
            if (HealthBar.CurrentHealth <= 0)
            {
                MarkedForDeletion = true;
                mapScene.GameOverlay.EnemiesKilled++;
                mapScene.GameOverlay.PlayerGold += GoldValue;
            }

            float minDistancePath = float.MaxValue;
            float minDistanceTower = float.MaxValue;
            Tile[,] tiles = mapScene.MapGrid.Tiles;
            Tile closestPathTile = null;
            Tile nextTile;

            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    float distance = Vector2.Distance(Position, tiles[x, y].TexturePosition);

                    if ((tiles[x, y] is PathTile || tiles[x, y] is NexusTile) &&
                        distance < minDistancePath && 
                        !CompletedTileList.Contains(tiles[x, y]))
                    {
                        minDistancePath = distance;
                        closestPathTile = tiles[x, y];
                    }


                }
            }

            HandleMovementToNextTile(closestPathTile, Math.Min(minDistancePath, minDistanceTower), mapScene);
        }

        private void HandleMovementToNextTile(Tile nextTile, float nextMinDistance, PlayMapScene mapScene)
        {
            Vector2 direction = Vector2.Normalize(nextTile.TexturePosition - Position);

            Velocity = direction * Speed;

            if (nextMinDistance < nextTile.TextureResizeDimension / 2 && !IsAttacking)
            {
                if (nextTile is NexusTile)
                {
                    MarkedForDeletion = true;
                    mapScene.GameOverlay.SubtractHeart();
                }
                else
                {
                    CompletedTileList.Add(nextTile);
                }
            }

            if (!IsAttacking) Position += Velocity;
            else Velocity = Vector2.Zero;
        }

        public virtual void Draw(PlayMapScene mapScene)
        {
            HealthBar.Draw(Position, Size);

            Globals.SpriteBatch.Draw(
                Texture, 
                new Rectangle(
                    (int)Position.X - Size / 2 + 5, //5 hjälper men vet inte varför den behövs 
                    (int)Position.Y - Size / 2,
                    Size, 
                    Size), 
                Color.White);
        }
    }
}