using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Grupp5Game
{
    public class BuildingTile : Tile
    {
        
        private float Range; 
        public int Damage { get; set; }
        public TimeSpan ShotDelay { get; set; }
        private TimeSpan timeSinceLastShot = TimeSpan.Zero;
        private TimeSpan TimeAtLastShot;
        private bool CanShoot = true;
        public List<Tuple<Vector2, bool>> AttackingPositions {  get; set; }
        public BuildingTile(int x, int y, float range) : base(x, y)
        {
            Texture = Assets.BasetowerTexture;
            Range = range;
               
            ShotDelay = TimeSpan.FromSeconds(0.6);
            Damage = 10;
            Texture = Assets.BasetowerTexture;
            AttackingPositions = new List<Tuple<Vector2, bool>>();

            Vector2 v1 = new Vector2(TexturePosition.X + 30, TexturePosition.Y + 30);
            Vector2 v2 = new Vector2(TexturePosition.X, TexturePosition.Y - 30);
            Vector2 v3 = new Vector2(TexturePosition.X - 30, TexturePosition.Y + 30);

            AttackingPositions.Add(Tuple.Create(v1, false));
            AttackingPositions.Add(Tuple.Create(v2, false));
            AttackingPositions.Add(Tuple.Create(v3, false));
        
        }
      
        private void UpdateTimeSinceLastShot(GameTime gameTime) 
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

            Enemy targetEnemy = GetTargetEnemy();
        
            if (CanShoot && targetEnemy != null)
            {
                CanShoot = false;
                TimeAtLastShot = gameTime.TotalGameTime;
                Vector2 direction = Vector2.Normalize(targetEnemy.Position - TexturePosition);
                CannonBall cannonBall = new CannonBall(TexturePosition, direction, Assets.SandTexture, Damage);
                PlayMapScene.Projectiles.Add(cannonBall);
            }
        }
    }    
}
