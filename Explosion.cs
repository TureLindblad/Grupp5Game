using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp5Game
{
    public class Explosion
    {
        public Vector2 Position { get; set; }
        public int Diameter { get; set; }
        public bool MarkedForDeletion { get; set; }
        public Animation animation { get; set; }

        public Explosion(Vector2 position, int explosionDamage, int diameter, PlayMapScene mapScene)
        {
            Position = position;
            MarkedForDeletion = false;
            Diameter = diameter;
            DamageEnemies(mapScene.EnemyList, explosionDamage);
            DeletionTimer();
            animation = new Animation(Assets.ExplosionAtlas, 8);
        }

        private async void DeletionTimer()
        {
            await Task.Delay(400);
            MarkedForDeletion = true;
        }

        private void DamageEnemies(List<Enemy> enemyList, int explosionDamage)
        {
            foreach (Enemy enemy in enemyList)
            {
                if (Vector2.Distance(Position, enemy.Position) <= Diameter / 2)
                {
                    enemy.HealthBar.CurrentHealth -= explosionDamage;
                }
            }
        }

        public void Draw()
        {
            animation.Draw(new Rectangle((int)Position.X - Diameter/ 2, (int)Position.Y - Diameter / 2, Diameter, Diameter));
        }
    }
}
