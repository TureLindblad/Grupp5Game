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
        public Rectangle Bounds { get { 
                return new Rectangle((int)Position.X - Size / 2, (int)Position.Y - Size / 2 , Size , Size); 
            } 
        }
        public Rectangle BoundsWithNextMovement
        {
            get
            {
                int xMod = (int)(Velocity.X);
                int yMod = (int)(Velocity.Y);
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
        protected bool AttacksTower { get; set; }
        protected bool IsAttacking { get; set; }
        public Enemy(Texture2D texture)
        {
            Texture = texture;

            Position = new Vector2(10, 10);

            Velocity = new Vector2(0, 0);

            CompletedTileList = new List<Tile>();
        }

        public void Update(MapScene mapScene)
        {
            float minDistancePath = float.MaxValue;
            float minDistanceTower = float.MaxValue;
            Tile[,] tiles = mapScene.MapGrid.Tiles;
            Tile closestPathTile = null;
            TowerTile closestTowerTile = null;
            Tile nextTile;

            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < mapScene.MapGrid.Tiles.GetLength(1); y++)
                {
                    float distance = Vector2.Distance(Position, tiles[x, y].TexturePosition);
                    if (tiles[x, y].IsPath && distance < minDistancePath && !CompletedTileList.Contains(tiles[x, y]))
                    {
                        minDistancePath = distance;
                        closestPathTile = tiles[x, y];
                    }

                    if (tiles[x, y] is TowerTile towerTile && distance < minDistanceTower/* && CheckAttackingPositions(towerTile)*/)
                    {
                        minDistanceTower = distance;
                        if (minDistanceTower < tiles[0,0].TextureResizeDimension * 1.3)
                        {
                            closestTowerTile = towerTile;
                        }
                    }
                }
            }

            if (closestTowerTile != null && AttacksTower )
            {
                nextTile = closestTowerTile;
            }
            else nextTile = closestPathTile;

            if (nextTile != null)
            {
                Vector2 direction = Vector2.Normalize(nextTile.TexturePosition - Position);

                Velocity = direction * Speed;

                if (minDistancePath < nextTile.TextureResizeDimension / 2 )
                {
                    CompletedTileList.Add(nextTile);
                }

                if (nextTile is TowerTile nextTower && minDistanceTower < nextTile.TextureResizeDimension / 2)
                {
                    foreach(var li in nextTower.AttackingPositions)
                    {
                        if (!li.Item2)
                        {

                            Position = li.Item1; // Assign the enemy's position
                                                    // Update the availability of this position in nextTower.AttackingPositions
                            nextTower.AttackingPositions[nextTower.AttackingPositions.IndexOf(li)] = Tuple.Create(li.Item1, true);
                            IsAttacking = true;
                            break;
                        }
                    }
                    /*tiles[nextTile.IndexPosition.X, nextTile.IndexPosition.Y] =
                        new Tile(nextTile.IndexPosition.X, nextTile.IndexPosition.Y, false);*/
                    
                }

                Tuple<bool, bool> collisionChecks = Collision.CheckCollision(this, mapScene.EnemyList);

                if (collisionChecks.Item1)
                {
                    Velocity = new Vector2(0, Velocity.Y + Speed * direction.X);
                }
                
                if (collisionChecks.Item2)
                {
                    Velocity = new Vector2(Velocity.X + Speed * direction.Y, 0);
                }

                if (!IsAttacking) Position += Velocity;
            }

            //if (CompletedTileList.Count == mapScene.MapGrid.NumberOfPathTiles) mapScene.EnemyList.Remove(this);
        }
        private static bool CheckAttackingPositions(TowerTile towerTile)
        {
            foreach (var li in towerTile.AttackingPositions)
            {
                if (!li.Item2) return true;
            }

            return false;
        }

        public void Draw(MapScene mapScene)
        {
            Globals.SpriteBatch.Draw(Assets.IntroTextTexture, BoundsWithNextMovement,
                    Color.Black);

            Globals.SpriteBatch.Draw(
                Texture, 
                new Rectangle(
                    (int)Position.X - Size / 2/* + 5*/, //5 hjälper men vet inte varför den behövs 
                    (int)Position.Y - Size / 2/* - 15*/, //-15 för att rita lite över mitten
                    Size, 
                    Size), 
                Color.White);

            
        }
    }
}