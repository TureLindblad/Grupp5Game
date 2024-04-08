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
                        if (projectile is MagicProjectile)
                        {
                            FinalDamage = ((projectile.MagicDamage - enemy.MagicArmor)
                                          + (projectile.MagicDamage / 2));

                            if(FinalDamage > 0) 
                            {
                                enemy.HealthBar.CurrentHealth -= FinalDamage;

                                _ = projectile.ApplyProjectileEffect(enemy);
                            }
                            else if(FinalDamage < 0)
                            {
                                FinalDamage = projectile.MagicDamage / 2;
                                enemy.HealthBar.CurrentHealth -= FinalDamage;
                            }
                        }

                        if (projectile is Arrow)
                        {
                            FinalDamage = ((projectile.PhysDamage - enemy.PhysArmor)
                                          + (projectile.PhysDamage / 2));

                            if (FinalDamage > 0 && !enemy.IsBurning)
                            {
                                enemy.HealthBar.CurrentHealth -= FinalDamage;

                                _ = projectile.ApplyProjectileEffect(enemy);
                            }

                            else if (FinalDamage < 0 && !enemy.IsBurning)
                            {
                                FinalDamage = projectile.PhysDamage / 2;
                                enemy.HealthBar.CurrentHealth -= FinalDamage;
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



