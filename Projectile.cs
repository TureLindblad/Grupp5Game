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
        public Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)Position.X - Size / 2, (int)Position.Y - Size / 2, Size, Size);
            }
        }
        public Vector2 Position { get; set; }
        public int Size { get; set; }
        public Vector2 Direction { get; set; }
        public float Speed { get; set; }
        public Texture2D Texture { get; set; }
        public int Damage { get; set; }
        public float Rotation { get; set; }
        public Projectile() 
        {


        }

        public virtual void Update(List<Enemy> enemyList)
        {
            if (Collision.CheckCollisionAndDamageEnemy(this, enemyList))
            {
                PlayMapScene.Projectiles.Remove(this);
            }
        }
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

        public override void Update(List<Enemy> enemyList)
        {
            base.Update(enemyList);

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
            Speed = 20f;
            Texture = texture;
            Damage = damage;
        }

        public override void Update(List<Enemy> enemyList)
        {
            base.Update(enemyList);

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

        public override void Update(List<Enemy> enemyList)
        {
            base.Update(enemyList);

            Position += Direction * Speed;
            Rotation = (float)Math.Atan2(Direction.Y, Direction.X);
        }
    }
}