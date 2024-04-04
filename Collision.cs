using System.Collections.Generic;

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
                    if (projectile is MagicProjectile)
                    {
                        enemy.HealthBar.CurrentHealth -= ((projectile.MagicDamage - enemy.MagicArmor)
                                                           + (projectile.MagicDamage / 2));
                        return true;
                    }
                    if (projectile is Arrow)
                    {
                        enemy.HealthBar.CurrentHealth -= ((projectile.PhysDamage - enemy.PhysArmor)
                                                           + (projectile.PhysDamage / 2));
                        return true;
                    }
                }
            }

            return false;
        }
    }
}


