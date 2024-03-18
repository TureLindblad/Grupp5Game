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

        public void Update(Game1 game, GameTime gameTime)
        {
            float minDistance = float.MaxValue;
            Tile[,] tiles = game.MapGrid.Tiles;
            Tile closestTile = null;

            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < game.MapGrid.Tiles.GetLength(1); y++)
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

            if (CompletedTileList.Count == game.MapGrid.NumberOfPathTiles) game.EnemyList.Remove(this);
        }

        public void Draw(Game1 game)
        {
            game._spriteBatch.Draw(
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