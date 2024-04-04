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
        public bool MarkedForDeletion { get; set; }

        public Explosion(Vector2 position)
        {
            Position = position;
            MarkedForDeletion = false;
            DeletionTimer();
        }

        private async void DeletionTimer()
        {
            await Task.Delay(200);
            MarkedForDeletion = true;
        }

        public void Draw()
        {
            Globals.SpriteBatch.Draw(Assets.ExplosionTexture, new Rectangle(
                (int)Position.X - CannonBall.SplashDiameter / 2,
                (int)Position.Y - CannonBall.SplashDiameter / 2,
                CannonBall.SplashDiameter,
                CannonBall.SplashDiameter),
                Color.White);
        }
    }
}
