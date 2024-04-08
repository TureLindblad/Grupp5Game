using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp5Game
{
    public class MagicBolt
    {
        public Vector2 Position { get; set; }
        public int Diameter { get; set; }
        public bool MarkedForDeletion { get; set; }
        public Animation animation { get; set; }

        public MagicBolt(Vector2 position, int magicDamage, int diameter, PlayMapScene mapScene) 
        {
            Position = position;
            MarkedForDeletion = false;
            Diameter = diameter;
            DamageEnemies(mapScene.EnemyList, magicDamage);
            DeletionTimer();
            animation = new Animation(Assets.MagicBoltAtlas, 11);
        }
        private async void DeletionTimer()
        {
            await Task.Delay(300);
            MarkedForDeletion = true;
        }

        private void DamageEnemies(List<Enemy> enemyList, int magicDamage)
        {
            foreach (Enemy enemy in enemyList)
            {
                if (Vector2.Distance(Position, enemy.Position) <= Diameter / 2)
                {
                    enemy.HealthBar.CurrentHealth -= magicDamage;
                }
            }
        }

        public void Draw()
        {
            animation.Draw(new Rectangle((int)Position.X - Diameter / 2, (int)Position.Y - Diameter / 2, Diameter, Diameter));
        }
    }
}

