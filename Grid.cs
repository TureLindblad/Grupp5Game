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
        public readonly int MaxNumberOfPathTiles = 27;
        public List<Tile> PathTileOrder { get; private set; }

        private MouseState LastMouseState { get; set; }
        private MouseState CurrentMouseState { get; set; }

        public Grid(Point gridDimensions, bool loadReadyMap)
        {
            Tiles = new Tile[gridDimensions.X, gridDimensions.Y];

            for (int x = 0; x < Tiles.GetLength(0); x++)
            {
                for (int y = 0; y < Tiles.GetLength(1); y++)
                {
                    if (FromMatrixIsPath(Assets.BigGrid25x12[y * 25 + x]) && loadReadyMap)
                    {
                        NumberOfPathTiles++;
                        Tiles[x, y] = new PathTile(x, y, Assets.BigGrid25x12[y * 25 + x] - '0');
                    }
                    else
                    {
                        Tiles[x, y] = new TerrainTile(x, y);
                    }
                }
            }

            PathTileOrder = new List<Tile>();

            Tiles[0, 0] = new PathTile(0, 0, 1);
            PathTileOrder.Add(Tiles[0, 0]);
        }

        private static bool FromMatrixIsPath(char c)
        {
            if (c == '0') return false;
            else return true;
        }

        public void Update()
        {
            LastMouseState = CurrentMouseState;
            CurrentMouseState = Mouse.GetState();
            
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

            if (minDistance < Math.Max(selected.Origin.X, selected.Origin.Y))
            {
                selected.TileColor = Color.Lerp(Color.White, Color.Gray, 0.5f);
            }

            if (CurrentMouseState.LeftButton == ButtonState.Pressed && 
                LastMouseState.LeftButton == ButtonState.Released &&
                selected is not PathTile &&
                CheckNumberOfAdjacentPathTiles(selected) &&
                NumberOfPathTiles < MaxNumberOfPathTiles)
            {
                Tiles[selected.IndexPosition.X, selected.IndexPosition.Y] 
                    = new PathTile(selected.IndexPosition.X, selected.IndexPosition.Y, 1);

                PathTileOrder.Add(Tiles[selected.IndexPosition.X, selected.IndexPosition.Y]);

                NumberOfPathTiles++;
            }

            if (Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                Tiles[selected.IndexPosition.X, selected.IndexPosition.Y] = new TowerTile(selected.IndexPosition.X, selected.IndexPosition.Y);
            }
        }

        private bool CheckNumberOfAdjacentPathTiles(Tile selected)
        {
            int adjacentTiles = 0;
            bool foundAdjacentFirst = false;

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    try
                    {
                        if (Tiles[selected.IndexPosition.X + x, selected.IndexPosition.Y + y] is PathTile /*&&
                            ((x != -1 && y != -1) && (x != 1 && y != -1))*/)
                        {
                            adjacentTiles++;
                        }

                        if (Tiles[selected.IndexPosition.X + x, selected.IndexPosition.Y + y] == PathTileOrder.Last())
                        {
                            foundAdjacentFirst = true;
                        }
                    }
                    catch { }
                }
            }

            return foundAdjacentFirst && adjacentTiles <= 2;
        }

        public void Draw()
        {
            for (int x = 0; x < Tiles.GetLength(0); x++)
            {
                for (int y = 0; y < Tiles.GetLength(1); y++)
                {
                    Tiles[x, y].Draw();
                }
            }

            Globals.SpriteBatch.DrawString(
                Assets.IntroTextFont,
                "Number of tiles left: " + (MaxNumberOfPathTiles - NumberOfPathTiles),
                new Vector2(Globals.WindowSize.X / 2 - 80, Globals.WindowSize.Y - 60),
                Color.Red);
        }
    }
}
