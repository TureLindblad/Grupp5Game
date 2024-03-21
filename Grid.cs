using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp5Game
{
    public class Grid
    {
        public Tile[,] Tiles { get; set; }
        public int NumberOfPathTiles { get; private set; }

        public Grid(Point gridDimensions)
        {
            Tiles = new Tile[gridDimensions.X, gridDimensions.Y];

            bool isPath = false;

            for (int x = 0; x < Tiles.GetLength(0); x++)
            {
                for (int y = 0; y < Tiles.GetLength(1); y++)
                {
                    if (FromMatrixIsPath(Assets.GridMatrix2[y * 19 + x]))
                    {
                        NumberOfPathTiles++;
                        isPath = true;
                    }

                    Tiles[x, y] = new Tile(x, y, isPath);

                    isPath = false;
                }
            }
        }

        private static bool FromMatrixIsPath(char c)
        {
            if (c == '0') return false;
            else return true;
        }

        public void Update()
        {
            Vector2 MousePosition = Mouse.GetState().Position.ToVector2();
            float minDistance = float.MaxValue;
            Tile selected = null;

            for (int x = 0; x < Tiles.GetLength(0); x++)
            {
                for (int y = 0; y < Tiles.GetLength(1); y++)
                {
                    Tiles[x, y].Update();

                    float distance = Vector2.Distance(MousePosition, Tiles[x, y].TexturePosition);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        selected = Tiles[x, y];
                    }
                }
            }

    if (minDistance < Math.Max(selected.Origin.X, selected.Origin.Y) &&
        Mouse.GetState().LeftButton == ButtonState.Pressed)
    {
        if (selected.IsPath)
        {
            
            return;
        }

        bool canPlaceTower = false;
        foreach (var neighbor in GetNeighborTiles(selected.IndexPosition))
        {
            if (neighbor.IsPath)
            {
                canPlaceTower = true; 
                break;
            }
        }

        if (canPlaceTower)
        {
            Tiles[selected.IndexPosition.X, selected.IndexPosition.Y] = new TowerTile(selected.IndexPosition.X, selected.IndexPosition.Y, false);
        }
    }
}

        private IEnumerable<Tile> GetNeighborTiles(Point position)
        {
            var neighbors = new List<Tile>();

            if (position.X > 0)
                neighbors.Add(Tiles[position.X - 1, position.Y]);
            if (position.X < Tiles.GetLength(0) - 1)
                neighbors.Add(Tiles[position.X + 1, position.Y]);
            if (position.Y > 0)
                neighbors.Add(Tiles[position.X, position.Y - 1]);
            if (position.Y < Tiles.GetLength(1) - 1)
                neighbors.Add(Tiles[position.X, position.Y + 1]);

            return neighbors;
        }  

        public void Draw(Game1 game)
        {
            for (int x = 0; x < Tiles.GetLength(0); x++)
            {
                for (int y = 0; y < Tiles.GetLength(1); y++)
                {
                    Tiles[x, y].Draw(game);
                }
            }
        }
    }
}
