using Microsoft.Xna.Framework.Graphics;

namespace Grupp5Game
{
    public class Tier2Enemy : Enemy
    {
        public Tier2Enemy(Texture2D texture) : base(texture)
        {

        }
    }

    public class FrostEnemy2 : Tier2Enemy
    {
        public FrostEnemy2(Texture2D texture) : base(texture)
        {
            Size = 50;
            HealthBar = new HealthBar(55);
            Speed = 2;
            MagicArmor = 5;
            PhysArmor = 10;
            GoldValue = 100;
        }
    }
    public class FireEnemy2 : Tier2Enemy
    {
        public FireEnemy2(Texture2D texture) : base(texture)
        {
            Size = 50;
            HealthBar = new HealthBar(55);
            Speed = 2;
            PhysArmor = 10;
            MagicArmor = 5;
            GoldValue = 100;
        }
    }
}
