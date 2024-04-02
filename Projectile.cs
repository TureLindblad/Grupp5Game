using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace Grupp5Game
{
    public abstract class Projectile 
    {

        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }
        public float Speed { get; set; }
        public Texture2D Texture { get; set; }
        public int Damage { get; set; }
        public float Rotation { get; set; }
        public Projectile() 
        {


        }

        public abstract void Update();
        public void Draw()
        {
            Globals.SpriteBatch.Draw(Texture, new Rectangle((int)Position.X, (int)Position.Y, 10, 10), Color.Black);
        }
    }
    public class Arrow : Projectile
    {

        public Arrow(Vector2 position, Vector2 direction, Texture2D texture, int damage)
        {
            Position = position;
            Direction = direction;
            Speed = 15f;
            Texture = texture;
            Damage = damage;
        }

        public override void Update()
        {
            Position += Direction * Speed;
            Rotation = (float)Math.Atan2(Direction.Y, Direction.X);
        }
    }

    public class CannonBall : Projectile
    {

        public CannonBall(Vector2 position, Vector2 direction, Texture2D texture, int damage)
        {
            Position = position;
            Direction = direction;
            Speed = 5f;
            Texture = texture;
            Damage = damage;
        }

        public override void Update()
        {
            Position += Direction * Speed;
        }
    }

    public class MagicProjectile : Projectile
    {

        public MagicProjectile(Vector2 position, Vector2 direction, Texture2D texture, int damage)
        {
            Position = position;
            Direction = direction;
            Speed = 9f;
            Texture = texture;
            Damage = damage;
        }

        public override void Update()
        {
            Position += Direction * Speed;
            Rotation = (float)Math.Atan2(Direction.Y, Direction.X);
        }
    }
}