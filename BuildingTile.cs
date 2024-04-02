using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        public static readonly int TowerCost = 150;
        public ArcherTower(int x, int y) : base(x, y)
        {
            Texture = Assets.NexusTexture;
            Range = 200;
            ShotDelay = TimeSpan.FromSeconds(0.4);
            Damage = 5;
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
                PlayMapScene.Projectiles.Add(new Arrow(TexturePosition, direction, Assets.BasetowerTexture, Damage));
            }
        }
    }

    public class CannonTower : BuildingTile
    {
        public static readonly int TowerCost = 250;
        public CannonTower(int x, int y) : base(x, y)
        {
            Texture = Assets.BasetowerTexture;
            Range = 130;
            ShotDelay = TimeSpan.FromSeconds(1);
            Damage = 10;
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
                PlayMapScene.Projectiles.Add(new CannonBall(TexturePosition, direction, Assets.BasetowerTexture, Damage));
            }
        }
    }

    public class MagicTower : BuildingTile
    {
        public static readonly int TowerCost = 300;
        public MagicTower(int x, int y) : base(x, y)
        {
            Texture = Assets.NexusTextureOuter;
            Range = 180;
            ShotDelay = TimeSpan.FromSeconds(0.7);
            Damage = 7;
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
                PlayMapScene.Projectiles.Add(new MagicProjectile(TexturePosition, direction, Assets.BasetowerTexture, Damage));
            }
        }
    }
}
