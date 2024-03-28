using Microsoft.Xna.Framework;
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
            Position = new Vector2(0, 0);
            MaxHealth = 20;
            CurrentHealth = MaxHealth;
            Size = 30;
            Velocity = new Vector2(0, 0);
            Speed = 5;
            AttacksTower = false;
        }
    }
    public class FrostEnemy : Tier1Enemy
    {
        public FrostEnemy(Texture2D texture) : base(texture)
        {
            Position = new Vector2(0, 0);
            Size = 35;
            MaxHealth = 35;
            CurrentHealth = MaxHealth;
            Velocity = new Vector2(0, 0);
            Speed = 5;
            AttacksTower = true;
        }
    }
}
