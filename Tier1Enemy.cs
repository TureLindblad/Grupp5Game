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
            Size = 50;
            Velocity = new Vector2(0, 0);
            Speed = 5;
        }
    }
}
