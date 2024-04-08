using Microsoft.Xna.Framework.Graphics;

namespace Grupp5Game
{
    public class Tier3Enemy : Enemy
    {
        public Tier3Enemy(Texture2D texture) : base(texture) 
        {

        }
    }
    public class FireEnemy3 : Tier3Enemy
    {
        public FireEnemy3(Texture2D texture) : base(texture)
        {
            HealthBar = new HealthBar(200 * Globals.HealthMod);
            Size = 70;
            Speed = 2;
            PhysArmor = 10;
            GoldValue = 230;
        }
    }
    public class FrostEnemy3 : Tier3Enemy
    {
        public FrostEnemy3(Texture2D texture) : base(texture)
        {
            HealthBar = new HealthBar(200 * Globals.HealthMod);
            Size = 70;
            Speed = 2;
            PhysArmor = 10;
            GoldValue = 230;
        }
    }
}
