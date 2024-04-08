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
        
        public static float RainOfFireCooldown = 0;
        public static float FrostNovaCooldown = 0;
        public static async void SpawnManyExplosions(PlayMapScene mapScene)
        {
            RainOfFireCooldownTimer();

            Random rnd = new Random();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    int splashDiameter = 300;
                    mapScene.Explosions.Add(new Explosion(
                    new Vector2(
                        rnd.Next(splashDiameter / 2, Globals.WindowSize.X - splashDiameter / 2),
                        rnd.Next(splashDiameter / 2, Globals.WindowSize.Y - splashDiameter / 2))
                    , 1000000, splashDiameter, mapScene));
                    await Task.Delay(40);
                }

                await Task.Delay(150);
            }
        }

        public static void FreezeAllEnemies(PlayMapScene mapScene)
        {
            FrostNovaCooldownTimer();

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

        private static async void RainOfFireCooldownTimer()
        {
            RainOfFireCooldown = 1;

            while (RainOfFireCooldown > 0)
            {
                RainOfFireCooldown -= 0.01f;
                await Task.Delay(1000);
            }

            RainOfFireCooldown = 0;
        }

        private static async void FrostNovaCooldownTimer()
        {
            FrostNovaCooldown = 1;
            
            while (FrostNovaCooldown > 0)
            {
                FrostNovaCooldown -= 0.01f;
                await Task.Delay(500);
            }

            FrostNovaCooldown = 0;
        }

        public static void DrawFireRainCooldown()
        {
            Rectangle rainOfFireRect = new Rectangle(
                MapObjectContainer.RainOfFirePosition.X - MapObjectContainer.AbilitiesSize.X,
                MapObjectContainer.RainOfFirePosition.Y - MapObjectContainer.AbilitiesSize.Y,
                (int)(MapObjectContainer.AbilitiesSize.X * RainOfFireCooldown),
                MapObjectContainer.AbilitiesSize.Y);

            Globals.SpriteBatch.Draw(Assets.RainOfFire, rainOfFireRect, Color.Black * 0.5f);
        }

        public static void DrawFrostNovaCooldown()
        {
            Rectangle frostNovaRect = new Rectangle(
                MapObjectContainer.FrostNovaPosition.X - MapObjectContainer.AbilitiesSize.X,
                MapObjectContainer.FrostNovaPosition.Y - MapObjectContainer.AbilitiesSize.Y,
                (int)(MapObjectContainer.AbilitiesSize.X * FrostNovaCooldown),
                MapObjectContainer.AbilitiesSize.Y);

            Globals.SpriteBatch.Draw(Assets.FrostNova, frostNovaRect, Color.Black * 0.5f);
        }
    }
}
