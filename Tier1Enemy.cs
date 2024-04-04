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
           
        }
    }
    public class FireEnemy : Tier1Enemy
    {
        public FireEnemy(Texture2D texture) : base(texture)
        {
            Size = 35;
            HealthBar = new HealthBar(35);
            Speed = 2;
            MagicArmor = 5;
            GoldValue = 50;
        }
    }
}
