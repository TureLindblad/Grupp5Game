using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Grupp5Game
{
    public abstract class BuildingTile : Tile
    {
        
        protected float Range; 
        public int Damage { get; set; }
        public TimeSpan ShotDelay { get; set; }
        protected TimeSpan timeSinceLastShot = TimeSpan.Zero;
        protected TimeSpan TimeAtLastShot;
        protected bool CanShoot = true;
        public List<Tuple<Vector2, bool>> AttackingPositions {  get; set; }
        public BuildingTile(int x, int y) : base(x, y)
        {
            AttackingPositions = new List<Tuple<Vector2, bool>>();

            Vector2 v1 = new Vector2(TexturePosition.X + 30, TexturePosition.Y + 30);
            Vector2 v2 = new Vector2(TexturePosition.X, TexturePosition.Y - 30);
            Vector2 v3 = new Vector2(TexturePosition.X - 30, TexturePosition.Y + 30);

            AttackingPositions.Add(Tuple.Create(v1, false));
            AttackingPositions.Add(Tuple.Create(v2, false));
            AttackingPositions.Add(Tuple.Create(v3, false));
        }
      
        protected void UpdateTimeSinceLastShot(GameTime gameTime) 
        {
            timeSinceLastShot = gameTime.TotalGameTime - TimeAtLastShot;
        }

     
        public Enemy GetTargetEnemy()
        {
            if (Game1.CurrentScene is PlayMapScene mapScene)
            {
                foreach (Enemy enemy in mapScene.EnemyList)
                {
                    float distance = Vector2.Distance(TexturePosition, enemy.Position);
                    if (distance <= Range)
                    {
                        return enemy;
                    }
                }
            }
 
            return null;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            UpdateTimeSinceLastShot(gameTime);

            if (timeSinceLastShot >= ShotDelay) CanShoot = true;
        }
    }    

    public class ArcherTower : BuildingTile
    {
        private bool upgraded = false;
        public static readonly int TowerCost = 150;

        public ArcherTower(int x, int y) : base(x, y)
        {
            Texture = Assets.ArcherTowerTexture;
            Range = 200;
            ShotDelay = TimeSpan.FromSeconds(1.5);
            Damage = 5;
        }

        public void UpgradingTower()
        {
            if (!upgraded)
            {
                Damage = 35;
                ShotDelay = TimeSpan.FromSeconds(0.8);
                upgraded = true;
            }
        }

       

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Enemy targetEnemy = GetTargetEnemy();

            if (CanShoot && targetEnemy != null)
            {
                CanShoot = false;
                TimeAtLastShot = gameTime.TotalGameTime;

                Vector2 direction = Vector2.Normalize(
                    (targetEnemy.Position + targetEnemy.Velocity * Vector2.Distance(targetEnemy.Position, TexturePosition) / Arrow.Speed) - TexturePosition);

                PlayMapScene.Projectiles.Add(new Arrow(
                    TexturePosition + new Vector2(Arrow.ArrowSize / 2, Arrow.ArrowSize / 2), 
                    direction, Assets.ArrowTexture, Damage, upgraded));
            }
        }
    }

    public class CannonTower : BuildingTile
    {
        
        private bool upgraded = false;
        public static readonly int TowerCost = 250;

        public int SplashDiameter {get; set;}
       
        public CannonTower(int x, int y) : base(x, y)
        {
            Texture = Assets.CannonTowerTexture;
            Range = 130;
            ShotDelay = TimeSpan.FromSeconds(1.0);
            Damage = 10;
            SplashDiameter = 100;
            
            
            
        }
         public void UpgradingTower()
        {
            if (!upgraded)
            {
              
                Damage = 35;
                ShotDelay = TimeSpan.FromSeconds(0.5);
                upgraded = true;
                SplashDiameter = 400;

                
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Enemy targetEnemy = GetTargetEnemy();

            if (CanShoot && targetEnemy != null)
            {
                CanShoot = false;
                TimeAtLastShot = gameTime.TotalGameTime;

                Vector2 direction = Vector2.Normalize(
                    (targetEnemy.Position + targetEnemy.Velocity * Vector2.Distance(targetEnemy.Position, TexturePosition) / CannonBall.Speed) - TexturePosition);

                PlayMapScene.Projectiles.Add(new CannonBall(
                    TexturePosition + new Vector2(CannonBall.CannonBallSize / 2, CannonBall.CannonBallSize / 2), 
                    direction, Assets.CannonBallTexture, Damage, SplashDiameter));
            }
        }
    }

    public class MagicTower : BuildingTile
    {
        public bool magicProjectile = false;
        private bool upgraded = false;
        public static readonly int TowerCost = 300;
        public MagicTower(int x, int y) : base(x, y)
        {
            Texture = Assets.MagicTowerTexture;
            Range = 180;
            ShotDelay = TimeSpan.FromSeconds(1.0);
            Damage = 7;
        
        }

        public void UpgradingTower()
        {
            if (!upgraded)
            {
                Damage = 35;
                ShotDelay = TimeSpan.FromSeconds(0.7);
                upgraded = true;
                magicProjectile = true;

                
            }
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Enemy targetEnemy = GetTargetEnemy();

            if (CanShoot && targetEnemy != null)
            {
                CanShoot = false;
                TimeAtLastShot = gameTime.TotalGameTime;

                Vector2 direction = Vector2.Normalize(
                    (targetEnemy.Position + targetEnemy.Velocity * Vector2.Distance(targetEnemy.Position, TexturePosition) / MagicProjectile.Speed) - TexturePosition);

                PlayMapScene.Projectiles.Add(new MagicProjectile(
                    TexturePosition + new Vector2(MagicProjectile.MagicProjectileSize / 2, MagicProjectile.MagicProjectileSize / 2), 
                    direction, Assets.MagicProjectileTexture, Damage, upgraded));
            }
        }
    }
}
