using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Grupp5Game
{
    public class Cannontile : Tile
    {
        private List<Enemy> enemiesInRange;
        private float range; 

        public Cannontile (int x, int y, float range) : base(x, y)
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

      
    }
}
