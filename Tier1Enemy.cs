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
            MaxHealth = 20;
            CurrentHealth = MaxHealth;
            HealthBar = new HealthBar(25);
            Size = 30;
            Speed = 5;
            Speed = 3;
            AttacksTower = false;
        }
    }
    public class FrostEnemy : Tier1Enemy
    {
        public FrostEnemy(Texture2D texture) : base(texture)
        {
            Size = 35;
            HealthBar = new HealthBar(35);
            Speed = 2;
            AttacksTower = true;
        }
    }
    public class FireEnemy : Tier1Enemy
    {
        public FireEnemy(Texture2D texture) : base(texture)
        {
            Size = 35;
            MaxHealth = 35;
            CurrentHealth = MaxHealth;
            Speed = 5;
            HealthBar = new HealthBar(40);
            Speed = 4;
            AttacksTower = true;
        }
    }
}
