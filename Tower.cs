using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TD_shooter;

namespace Grupp5Game
{
    public class Tower
    {
        public Point Position { get; set; }
        public int Range { get; set; }

        public Tower(Point position, int range)
        {
            Position = position;
            Range = range;
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
    
    /*
    public class Tower
    {
        public Vector2 Position { get; set; }
        public TimeSpan ShotDelay { get; set; }
        public int Damage { get; set; }
        public float Range { get; set; }

        private TimeSpan timeSinceLastShot = TimeSpan.Zero;

        public Tower(Vector2 position, TimeSpan shotDelay, int damage, float range) // Uppdatera konstruktorn
        {
            Position = position;
            ShotDelay = shotDelay;
            Damage = damage;
            Range = range;
        }

        public virtual void Fire(Vector2 targetPosition, List<Arrow> arrows, Texture2D arrowTexture)
        {
        }
        public virtual void Fire(Vector2 targetPosition, List<CannonBall> cannonBalls, Texture2D cannonBallTexture)
        {
        }
        public virtual void Fire(Vector2 targetPosition, List<MagicProjectile> magicProjectiles, Texture2D magicProjectileTexture) // Lägg till en virtuell metod för magiprojektiler
        {
        }

        public void UpdateShotCooldown(GameTime gameTime)
        {
            timeSinceLastShot += gameTime.ElapsedGameTime;
        }

        public bool CanShoot()
        {
            return timeSinceLastShot >= ShotDelay;
        }

        public void ResetShotCooldown()
        {
            timeSinceLastShot = TimeSpan.Zero;
        }
    }

     public class ArcherTower : Tower
     {
        public ArcherTower(Vector2 position, TimeSpan shotDelay, int damage, float range) : base(position, shotDelay, damage, range)
        {
        }

        public override void Fire(Vector2 targetPosition, List<Arrow> arrows, Texture2D arrowTexture)
        {
            if (CanShoot())
            {
                Vector2 direction = Vector2.Normalize(targetPosition - Position);
                Arrow arrow = new Arrow(Position, direction, arrowTexture, Damage);
                arrows.Add(arrow);
                ResetShotCooldown();
            }
        }
     }

     public class CannonTower : Tower
     {
        public CannonTower(Vector2 position, TimeSpan shotDelay, int damage, float range) : base(position, shotDelay, damage, range)
        {
        }

        public override void Fire(Vector2 targetPosition, List<CannonBall> cannonBalls, Texture2D cannonBallTexture)
        {
            if (CanShoot())
            {
                Vector2 direction = Vector2.Normalize(targetPosition - Position);
                CannonBall cannonBall = new CannonBall(Position, direction, cannonBallTexture, Damage);
                cannonBalls.Add(cannonBall);
                ResetShotCooldown();
            }
        }
     }

     public class MageTower : Tower
     {
        public MageTower(Vector2 position, TimeSpan shotDelay, int damage, float range) : base(position, shotDelay, damage, range)
        {
        }

        public override void Fire(Vector2 targetPosition, List<MagicProjectile> magicProjectiles, Texture2D magicProjectileTexture)
        {
            if (CanShoot())
            {
                Vector2 direction = Vector2.Normalize(targetPosition - Position);
                MagicProjectile magicProjectile = new MagicProjectile(Position, direction, magicProjectileTexture, Damage);
                magicProjectiles.Add(magicProjectile);
                ResetShotCooldown();
            }
        }
     }

    } 
    */

}
