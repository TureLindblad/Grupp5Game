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
                    enemy.HealthBar.CurrentHealth -= projectile.Damage;
                    if (projectile is CannonBall)
                    {
                        _ = projectile.ApplyProjectileEffect(enemy, GetEnemiesForSplashDamage(enemy, enemyList));
                    }
                    else _ = projectile.ApplyProjectileEffect(enemy);

                    projectile.DamageAnimation(enemy);
                    
                    return true;
                }
            }

            return false;
        }

        private static List<Enemy> GetEnemiesForSplashDamage(Enemy targetEnemy, List<Enemy> otherEnemies)
        {
            List<Enemy> enemiesInRange = new List<Enemy>();

            foreach (Enemy enemy in otherEnemies)
            {
                if (Vector2.Distance(targetEnemy.Position, enemy.Position) <= CannonBall.SplashRadius)
                {
                    enemiesInRange.Add(enemy);
                }
            }

            return enemiesInRange;
        }
    }
}


