using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Grupp5Game
{
    public class BuildingTile : Tile
    {
        private List<Enemy> enemiesInRange;
        private float range; 

        public BuildingTile(int x, int y, bool isPath, float range) : base(x, y, isPath)
        {
            Texture = Assets.TowerBuildingTexture;
            enemiesInRange = new List<Enemy>();
            this.range = range;
        }

        public void AddEnemyInRange(Enemy enemy)
        {
            enemiesInRange.Add(enemy);
        }

        public void RemoveEnemyFromRange(Enemy enemy)
        {
            enemiesInRange.Remove(enemy);
        }

        public Enemy GetTargetEnemy()
        {
            
            foreach (Enemy enemy in enemiesInRange)
            {
                float distance = Vector2.Distance(TexturePosition, enemy.Position); 
                if (distance <= range)
                {
                    return enemy;
                }
            }

            
            return null;
        }

        public override void Draw()
        {
            Rectangle destinationRect = new Rectangle(
                (int)Math.Round(TexturePosition.X),
                (int)Math.Round(TexturePosition.Y),
                TextureResizeDimension,
                TextureResizeDimension);

            Globals.SpriteBatch.Draw(Texture, destinationRect, null, TileColor, 0f, Origin, SpriteEffects.None, 1f);
        }
    }
}
