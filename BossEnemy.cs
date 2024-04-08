using Microsoft.Xna.Framework.Graphics;

namespace Grupp5Game
{
    public class BossEnemy : Enemy
    {
        public BossEnemy(Texture2D texture) : base(texture) 
        {
            Size = 100;
            HealthBar = new HealthBar(2000 * Globals.HealthMod);
            Speed = 2;
            MagicArmor = 15;
            PhysArmor = 15;
            GoldValue = 450;
        }
    }
}
