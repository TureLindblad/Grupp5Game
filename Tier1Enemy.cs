using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Net.Mime;

namespace Grupp5Game
{
    public class Tier1Enemy : Enemy
    {
        protected AnimationManager animationManager;
        public Tier1Enemy(Texture2D texture) : base (texture) 
        {
            
        }
    }

    public class GoblinEnemy : Tier1Enemy
    {
        public GoblinEnemy(Texture2D texture) : base (texture) 
        {
            Position = new Vector2(0, 0);
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
            int frameCount = 6;
            Animation animation = new Animation(texture, frameCount);
            animationManager = new AnimationManager(animation);

            Position = new Vector2(0, 0);
            Size = 35;
            Velocity = new Vector2(0, 0);
            Speed = 5;
            AttacksTower = true;
           
        }

        
    }
}
