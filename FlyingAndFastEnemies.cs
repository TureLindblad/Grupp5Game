using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Grupp5Game
{
    public class FlyingAndFastEnemies : Enemy
    {
        public FlyingAndFastEnemies(Texture2D texture) : base(texture)
        {

        }
    }
    public class FlyingEnemy : FlyingAndFastEnemies
    {
        public FlyingEnemy(Texture2D texture) : base(texture)
        {
            HealthBar = new HealthBar(15);
            Size = 30;
            Speed = 5;
            GoldValue = 25;
        }

    }
    public class FastEnemy : FlyingAndFastEnemies
    {
        public FastEnemy(Texture2D texture) : base(texture)
        {
            HealthBar = new HealthBar(15);
            Size = 30;
            Speed = 6;
            GoldValue = 25;
        }
    }
}
