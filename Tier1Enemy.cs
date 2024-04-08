using Microsoft.Xna.Framework.Graphics;

namespace Grupp5Game
{
    public class Tier1Enemy : Enemy
    {
        public Tier1Enemy(Texture2D texture) : base (texture) 
        {

        }
    }

    public class GoblinEnemy : Tier1Enemy
    {
        public GoblinEnemy(Texture2D texture) : base (texture) 
        {
            HealthBar = new HealthBar(25);
            Size = 30;
            Speed = 3;
            PhysArmor = 5;
            GoldValue = 50;
        }
    }
    public class FrostEnemy : Tier1Enemy
    {
        public FrostEnemy(Texture2D texture) : base(texture)
        {
            Size = 35;
            HealthBar = new HealthBar(35);
            Speed = 2;
            MagicArmor = 7;
            GoldValue = 50;
        }
    }
    public class FireEnemy : Tier1Enemy
    {
        public FireEnemy(Texture2D texture) : base(texture)
        {
            Size = 35;
            HealthBar = new HealthBar(40);
            Speed = 4;
            MagicArmor = 7;
            GoldValue = 8;
        }
    }
}
