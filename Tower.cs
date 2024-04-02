using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Grupp5Game
{
    public class Tower
    {
        public Point Position { get; set; }
        public int Range { get; set; }
        public int Health { get; set; }
        public int Attack { get; set; }

        public Tower(Point position, int range)
        {
            Position = position;
            Range = range;
            Health = 100; 
            Attack = 50; 
        }

        public void ShootAtEnemy(List<Enemy> enemies)
        {
            Enemy nearestEnemy = null;
            float nearestDistance = float.MaxValue;

            foreach (Enemy enemy in enemies)
            {
                float distance = Vector2.Distance(Position.ToVector2(), enemy.Position);
                if (distance < Range && distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestEnemy = enemy;
                }
            }

            if (nearestEnemy != null)
            {
               
            }
        }
    }
}
