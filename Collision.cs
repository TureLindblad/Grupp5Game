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
                    int FinalDamage;

                    if (projectile is not CannonBall)
                    {
                        if (projectile is MagicProjectile magicProjectile)
                        {
                            enemy.HealthBar.CurrentHealth -= ((projectile.MagicDamage - enemy.MagicArmor)
                                                               + (projectile.MagicDamage / 2));
                            if (magicProjectile.FromUpgraded)
                            {
                                _ = projectile.ApplyProjectileEffect(enemy);
                            }
                        }
                        if (projectile is Arrow arrowProjectile)
                        {
                            enemy.HealthBar.CurrentHealth -= ((projectile.PhysDamage - enemy.PhysArmor)
                                                               + (projectile.PhysDamage / 2));

                            if (arrowProjectile.FromUpgraded)
                            {
                                _ = projectile.ApplyProjectileEffect(enemy);
                            }
                        }
                    }

                    projectile.DamageAnimation(enemy);

                    return true;
                }
            }

            return false;
        }
    }
}



