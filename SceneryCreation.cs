using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp5Game
{
    public static class SceneryCreation
    {
        public static void CreateScenery(PlayMapScene mapScene)
        {
            Random rnd = new Random();

            for (int x = 0; x < Globals.MapDimensions.X; x++)
            {
                for (int y = 0; y < Globals.MapDimensions.Y; y++)
                {
                    if (!HasCloseTileOfType<PathTile>(mapScene, mapScene.MapGrid.Tiles[x, y], 1) &&
                        mapScene.MapGrid.Tiles[x, y] is not MountainTile &&
                        mapScene.MapGrid.Tiles[x, y] is not NexusTile)
                    {
                        Tile tile = mapScene.MapGrid.Tiles[x, y];
                        if (rnd.Next(0, 100) < 5 &&
                            !HasCloseTileOfType<OasisTile>(mapScene, mapScene.MapGrid.Tiles[x, y], 1))
                        {
                            mapScene.MapGrid.Tiles[x, y] = new OasisTile(tile.IndexPosition.X, tile.IndexPosition.Y);
                        }
                        else if (rnd.Next(0, 100) < 6)
                        {
                            mapScene.MapGrid.Tiles[x, y] = new SandMountainTile(tile.IndexPosition.X, tile.IndexPosition.Y);
                        }
                        else if (rnd.Next(0, 100) < 6)
                        {
                            mapScene.MapGrid.Tiles[x, y] = new SandRockTile(tile.IndexPosition.X, tile.IndexPosition.Y);
                        }

                        
                    }
                }
            }

            for (int x = 0; x < Globals.MapDimensions.X; x++)
            {
                for (int y = 0; y < Globals.MapDimensions.Y; y++)
                {
                    Tile tile = mapScene.MapGrid.Tiles[x, y];
                    if (HasCloseTileOfType<OasisTile>(mapScene, mapScene.MapGrid.Tiles[x, y], 1) &&
                        !HasCloseTileOfType<BedouinTile>(mapScene, mapScene.MapGrid.Tiles[x, y], 1) &&
                        rnd.Next(0, 100) < 20 &&
                        mapScene.MapGrid.Tiles[x, y] is not OasisTile && 
                        mapScene.MapGrid.Tiles[x, y] is not MountainTile &&
                        mapScene.MapGrid.Tiles[x, y] is not NexusTile)
                    {
                        mapScene.MapGrid.Tiles[x, y] = new BedouinTile(tile.IndexPosition.X, tile.IndexPosition.Y);
                    }
                }
            }
        }

        private static bool HasCloseTileOfType<T>(PlayMapScene mapScene, Tile selected, int range) where T : Tile
        {
            for (int x = 0; x < mapScene.MapGrid.Tiles.GetLength(0); x++)
            {
                for (int y = 0; y < mapScene.MapGrid.Tiles.GetLength(1); y++)
                {
                    float distance = Vector2.Distance(selected.TexturePosition, mapScene.MapGrid.Tiles[x, y].TexturePosition);
                    if (mapScene.MapGrid.Tiles[x, y] is T && distance < selected.TextureResizeDimension * (range + 0.2f))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
