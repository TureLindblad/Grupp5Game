using Microsoft.Xna.Framework.Graphics;

namespace Grupp5Game
{
    public class BossEnemy : Enemy
    {
        public BossEnemy(Texture2D texture) : base(texture) 
        {
            Size = 100;
            HealthBar = new HealthBar(1000);
            Speed = 2;
            MagicArmor = 50;
            PhysArmor = 50;
            GoldValue = 1000;
        }
    }
}
