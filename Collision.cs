using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp5Game
{
    public static class Collision
    {
    public static bool CheckCollision(Enemy enemy1 , List<Enemy> enemyList)
        {
            foreach (Enemy enemy2 in enemyList)
            {
                if (enemy1.Bounds.Intersects(enemy2.Bounds) && enemy1 != enemy2 )
                {
                    return true;
                }
            }
            return false;
        }
    }
}
