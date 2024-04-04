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
                    ));
                    await Task.Delay(40);
                }

                await Task.Delay(150);
            }
        }

        public static void FreezeAllEnemies(PlayMapScene mapScene)
        {

        }
    }
}
