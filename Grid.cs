using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Grupp5Game
{
    public class Grid
    {
        public Tile[,] Tiles { get; set; }
        public int NumberOfPathTiles { get; set; }
        public readonly int MaxNumberOfPathTiles = 27;
        public List<Tile> PathTileOrder { get; private set; }
        public static Point NexusIndex = new Point(Globals.MapDimensions.X / 2, 0);

        public Grid(bool loadReadyMap)
        {
            Tiles = new Tile[Globals.MapDimensions.X, Globals.MapDimensions.Y];

            for (int x = 0; x < Tiles.GetLength(0); x++)
            {
                for (int y = 0; y < Tiles.GetLength(1); y++)
                {
                    if (Assets.BigGrid25x12[y * 25 + x] != '0' && loadReadyMap)
                    {
                        NumberOfPathTiles++;
                        Tiles[x, y] = new PathTile(x, y);
                    }
                    else
                    {
                        Tiles[x, y] = new TerrainTile(x, y);
                    }
                }
            }

            PathTileOrder = new List<Tile>();

            Tiles[0, 0] = new PathTile(0, 0);
            PathTileOrder.Add(Tiles[0, 0]);

            Tiles[NexusIndex.X, NexusIndex.Y] = new NexusTile(NexusIndex.X, NexusIndex.Y);
        }

        public void Update()
        {
            Vector2 mousePosition = Mouse.GetState().Position.ToVector2();
            float minDistance = float.MaxValue;
            Tile selected = null;

            for (int x = 0; x < Tiles.GetLength(0); x++)
            {
                for (int y = 0; y < Tiles.GetLength(1); y++)
                {
                    Tiles[x, y].Update();

                    float distance = Vector2.Distance(mousePosition, Tiles[x, y].TexturePosition);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        selected = Tiles[x, y];
                    }
                }
            }

            if (Game1.CurrentScene is MapCreationScene)
            {
                MapCreationTool(selected);
            }
            else if (Game1.CurrentScene is PlayMapScene)
            {
                TowerPlacingTool(selected);
            }

            if (minDistance < Math.Max(selected.Origin.X, selected.Origin.Y))
            {
                selected.TileColor = Color.Lerp(Color.White, Color.Gray, 0.5f);
            }
        }

        private void TowerPlacingTool(Tile selected)
        {
            if (Mouse.GetState().RightButton == ButtonState.Pressed &&
                selected is not PathTile &&
                selected is not NexusTile)
            {
                foreach (var neighbor in GetNeighborTiles(selected.IndexPosition))
                {
                    if (neighbor is PathTile)
                    {
                        Tiles[selected.IndexPosition.X, selected.IndexPosition.Y] = new BuildingTile(selected.IndexPosition.X, selected.IndexPosition.Y, 10);
                        break;
                    }
                }
                
            }
        }

        private void MapCreationTool(Tile selected)
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed &&
                selected is not PathTile &&
                selected is not NexusTile &&
                CheckAdjacentPathTiles(selected) &&
                NumberOfPathTiles < MaxNumberOfPathTiles)
            {
                Tiles[selected.IndexPosition.X, selected.IndexPosition.Y]
                    = new PathTile(selected.IndexPosition.X, selected.IndexPosition.Y);

                PathTileOrder.Add(Tiles[selected.IndexPosition.X, selected.IndexPosition.Y]);

                NumberOfPathTiles++;
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

        public bool CheckAdjacentPathTiles(Tile selected)
        {
            int adjacentTiles = 0;
            bool lastPathIsAdjacent = false;

            for (int x = 0; x < Tiles.GetLength(0); x++)
            {
                for (int y = 0; y < Tiles.GetLength(1); y++)
                {
                    float distance = Vector2.Distance(selected.TexturePosition, Tiles[x, y].TexturePosition);
                    if (Tiles[x, y] is PathTile && distance < selected.TextureResizeDimension)
                    {
                        adjacentTiles++;
                    }
                    if (Tiles[x, y] == PathTileOrder.Last() && distance < selected.TextureResizeDimension)
                    {
                        lastPathIsAdjacent = true;
                    }
                }
            }

            return adjacentTiles == 1 && lastPathIsAdjacent;
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
        }
    }
}
