using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp5Game
{
    public static class SpecialAbilities
    {
        private static int CooldownBar = 100;
        public static async void SpawnManyExplosions(PlayMapScene mapScene)
        {
            Random rnd = new Random();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    mapScene.Explosions.Add(new Explosion(
                    new Vector2(
                        rnd.Next(CannonBall.SplashDiameter / 2, Globals.WindowSize.X - CannonBall.SplashDiameter / 2),
                        rnd.Next(CannonBall.SplashDiameter / 2, Globals.WindowSize.Y - CannonBall.SplashDiameter / 2))
                    , 100, 300, mapScene));
                    await Task.Delay(40);
                }

                await Task.Delay(150);
            }
        }

        public static void FreezeAllEnemies(PlayMapScene mapScene)
        {
            foreach (Enemy enemy in mapScene.EnemyList)
            {
                FreezeTimer(enemy);
            }
        }

        private static async void FreezeTimer(Enemy enemy)
        {
            int lastEnemySpeed = enemy.Speed;
            Color lastEnemyColor = enemy.TextureColor;
            enemy.Speed = 0;
            enemy.TextureColor = Color.Blue;

            await Task.Delay(5000);

            enemy.Speed = lastEnemySpeed;
            enemy.TextureColor = lastEnemyColor;
        }

        private static async void CooldownTimer(int time)
        {
            Task delay = Task.Delay(time);
            
            while (delay.IsCompleted)
            {
                await Task.Delay(50);
            }
        }

        public static void Draw()
        {
            Rectangle cooldownRect = new Rectangle(
                (int)100,
                (int)100,
                CooldownBar,
                20);

            Globals.SpriteBatch.Draw(Assets.NameBox, cooldownRect, Color.White);
        }
    }
}
