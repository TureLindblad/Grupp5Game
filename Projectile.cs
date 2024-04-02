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
        public Texture2D Texture { get; set; }
        public int Damage { get; set; }
        public float Rotation { get; set; }

        public virtual void Update(List<Enemy> enemyList)
        {
            if (Collision.CheckCollisionAndDamageEnemy(this, enemyList))
            {
                PlayMapScene.Projectiles.Remove(this);
            }
        }
        public void Draw()
        {
            Globals.SpriteBatch.Draw(Texture, new Rectangle(
                (int)Position.X - Size / 2, 
                (int)Position.Y - Size / 2, 
                Size, 
                Size), 
                Color.Red);
        }
    }
    public class Arrow : Projectile
    {
        public static readonly float Speed = 15f;
        public Arrow(Vector2 position, Vector2 direction, Texture2D texture, int damage)
        {
            Position = position;
            Direction = direction;
            Size = 15;
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
        public static readonly float Speed = 10f;
        public CannonBall(Vector2 position, Vector2 direction, Texture2D texture, int damage)
        {
            Position = position;
            Direction = direction;
            Size = 25;
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
        public static readonly float Speed = 9f;
        public MagicProjectile(Vector2 position, Vector2 direction, Texture2D texture, int damage)
        {
            Position = position;
            Direction = direction;
            Size = 20;
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