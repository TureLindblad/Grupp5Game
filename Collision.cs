using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp5Game
{
    public static class Collision
    {
        public static bool CheckCollisionAndDamageEnemy(Projectile projectile, List<Enemy> enemyList)
        {
            foreach (Enemy enemy in enemyList)
            {
                if (projectile.Bounds.Intersects(enemy.Bounds))
                {
                    if (projectile is not CannonBall)
                    {
                        enemy.HealthBar.CurrentHealth -= projectile.Damage;
                        _ = projectile.ApplyProjectileEffect(enemy);
                    }

                    projectile.DamageAnimation(enemy);
                    
                    return true;
                }
            }

            return false;
        }
    }
}


