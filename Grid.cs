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
        public static Point NexusIndex = new Point(Globals.MapDimensions.X / 2, 1);

        private static Point NexusCenterIndex = new Point(Globals.MapDimensions.X / 2, 1);
        public Dictionary<NexusTile, Point> OuterNexusTiles = new Dictionary<NexusTile, Point>();

        public Grid()
        {
            Tiles = new Tile[Globals.MapDimensions.X, Globals.MapDimensions.Y];

            for (int x = 0; x < Tiles.GetLength(0); x++)
            {
                for (int y = 0; y < Tiles.GetLength(1); y++)
                {
                    if (y == 0 || y == Globals.MapDimensions.Y - 1) Tiles[x, y] = new MountainTile(x, y);
                    else Tiles[x, y] = new GrassTile(x, y);
                }
            }

            PathTileOrder = new List<Tile>();

            Tiles[0, Globals.MapDimensions.Y / 2] = new PathTile(0, Globals.MapDimensions.Y / 2);
            PathTileOrder.Add(Tiles[0, Globals.MapDimensions.Y / 2]);

            SpawnInNexusTiles();
        }

        private void SpawnInNexusTiles()
        {
            Tiles[NexusCenterIndex.X, NexusCenterIndex.Y] = new NexusTile(NexusCenterIndex.X, NexusCenterIndex.Y, true);

            NexusTile nt1 = new NexusTile(NexusCenterIndex.X - 1, NexusCenterIndex.Y, false);
            NexusTile nt2 = new NexusTile(NexusCenterIndex.X + 1, NexusCenterIndex.Y, false);
            NexusTile nt3 = new NexusTile(NexusCenterIndex.X, NexusCenterIndex.Y + 1, false);

            OuterNexusTiles.Add(nt1, new Point(NexusCenterIndex.X - 1, NexusCenterIndex.Y));
            OuterNexusTiles.Add(nt2, new Point(NexusCenterIndex.X + 1, NexusCenterIndex.Y));
            OuterNexusTiles.Add(nt3, new Point(NexusCenterIndex.X, NexusCenterIndex.Y + 1));

            Tiles[NexusCenterIndex.X - 1, NexusCenterIndex.Y] = nt1;
            Tiles[NexusCenterIndex.X + 1, NexusCenterIndex.Y] = nt2;
            Tiles[NexusCenterIndex.X, NexusCenterIndex.Y + 1] = nt3;
        }

        public void Update(GameTime gameTime)
        {
            Vector2 mousePosition = Mouse.GetState().Position.ToVector2();
            float minDistance = float.MaxValue;
            Tile selected = null;

            for (int x = 0; x < Tiles.GetLength(0); x++)
            {
                for (int y = 0; y < Tiles.GetLength(1); y++)
                {
                    Tiles[x, y].Update(gameTime);

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
            if (Game1.CurrentScene is PlayMapScene mapScene && mapScene.GameOverlay.PlayerGold > 0)
            {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed &&
                    selected is not BuildingTile &&
                    selected is not PathTile &&
                    selected is not MountainTile &&
                    selected is not NexusTile)
                {
                    foreach (var neighbor in GetNeighborTiles(selected))
                    {
                        if (neighbor is PathTile)
                        {
                            if (mapScene.SelectedTowerToPlace == TowerTypes.Archer)
                            {
                                Tiles[selected.IndexPosition.X, selected.IndexPosition.Y] = new ArcherTower(selected.IndexPosition.X, selected.IndexPosition.Y);
                                mapScene.GameOverlay.PlayerGold -= ArcherTower.TowerCost;
                                break;
                            }
                            if (mapScene.SelectedTowerToPlace == TowerTypes.Cannon)
                            {
                                Tiles[selected.IndexPosition.X, selected.IndexPosition.Y] = new CannonTower(selected.IndexPosition.X, selected.IndexPosition.Y);
                                mapScene.GameOverlay.PlayerGold -= CannonTower.TowerCost;
                                break;
                            }
                            if (mapScene.SelectedTowerToPlace == TowerTypes.Magic)
                            {
                                Tiles[selected.IndexPosition.X, selected.IndexPosition.Y] = new MagicTower(selected.IndexPosition.X, selected.IndexPosition.Y);
                                mapScene.GameOverlay.PlayerGold -= MagicTower.TowerCost;
                                break;
                            }
                        }
                    }

                }
            }
        }

        private void MapCreationTool(Tile selected)
        {
            var neighbours = GetNeighborTiles(selected);

            if (Mouse.GetState().LeftButton == ButtonState.Pressed &&
                selected is not PathTile &&
                selected is not MountainTile &&
                selected is not NexusTile &&
                neighbours.Count == 1 &&
                neighbours.Contains(PathTileOrder.Last()) &&
                NumberOfPathTiles < MaxNumberOfPathTiles)
            {
                Tiles[selected.IndexPosition.X, selected.IndexPosition.Y]
                    = new PathTile(selected.IndexPosition.X, selected.IndexPosition.Y);

                PathTileOrder.Add(Tiles[selected.IndexPosition.X, selected.IndexPosition.Y]);

                NumberOfPathTiles++;
            }
        }
        public List<Tile> GetNeighborTiles(Tile selected)
        {
            var neighbors = new List<Tile>();

            for (int x = 0; x < Tiles.GetLength(0); x++)
            {
                for (int y = 0; y < Tiles.GetLength(1); y++)
                {
                    float distance = Vector2.Distance(selected.TexturePosition, Tiles[x, y].TexturePosition);
                    if (Tiles[x, y] is PathTile && distance < selected.TextureResizeDimension)
                    {
                        neighbors.Add(Tiles[x, y]);
                    }
                }
            }

            return neighbors;
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
