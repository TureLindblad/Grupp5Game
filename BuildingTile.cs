using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Grupp5Game
{
    public class BuildingTile : Tile
    {
        private List<Enemy> enemiesInRange;
        private float Range; 
        public int Damage { get; set; }
        public TimeSpan ShotDelay { get; set; }
        private TimeSpan timeSinceLastShot = TimeSpan.Zero;
        public List<Tuple<Vector2, bool>> AttackingPositions {  get; set; }
        public BuildingTile(int x, int y, float range) : base(x, y)
        {
            Texture = Assets.BasetowerTexture;
            enemiesInRange = new List<Enemy>();
            Range = range;
               
            ShotDelay = new TimeSpan(100);
            Damage = 10;
            Texture = Assets.TowerTexture;
            AttackingPositions = new List<Tuple<Vector2, bool>>();

            Vector2 v1 = new Vector2(TexturePosition.X + 30, TexturePosition.Y + 30);
            Vector2 v2 = new Vector2(TexturePosition.X, TexturePosition.Y - 30);
            Vector2 v3 = new Vector2(TexturePosition.X - 30, TexturePosition.Y + 30);

            AttackingPositions.Add(Tuple.Create(v1, false));
            AttackingPositions.Add(Tuple.Create(v2, false));
            AttackingPositions.Add(Tuple.Create(v3, false));
        
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
        public void AddEnemyInRange(Enemy enemy)
        {
            enemiesInRange.Add(enemy);
        }

        public void RemoveEnemyFromRange(Enemy enemy)
        {
            enemiesInRange.Remove(enemy);
        }

        public Enemy GetTargetEnemy()
        {
            
            foreach (Enemy enemy in enemiesInRange)
            {
                float distance = Vector2.Distance(TexturePosition, enemy.Position); 
                if (distance <= Range)
                {
                    return enemy;
                }
            }
 
 
            return null;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            UpdateShotCooldown(gameTime);

            Enemy targetEnemy = GetTargetEnemy();
        
            if (/*CanShoot() && */targetEnemy != null)
            {
                Vector2 direction = Vector2.Normalize(targetEnemy.Position - TexturePosition);
                CannonBall cannonBall = new CannonBall(TexturePosition, direction, Assets.NexusTexture, Damage);
                PlayMapScene.Projectiles.Add(cannonBall);
                ResetShotCooldown();
            }
        }
    }    
}
