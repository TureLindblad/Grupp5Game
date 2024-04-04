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
        public int PhysDamage { get; set; }
        public int MagicDamage { get; set; }
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
                null,
                Color.White,
                Rotation,
                new Vector2(Texture.Width / 2, Texture.Height / 2),
                SpriteEffects.None,
                1f);
        }
    }
    public class Arrow : Projectile
    {
        public static readonly float Speed = 15f;
        public static readonly int ArrowSize = 40;
        public Arrow(Vector2 position, Vector2 direction, Texture2D texture, int damage)
        {
            Position = position;
            Direction = direction;
            Size = ArrowSize;
            Texture = texture;
            PhysDamage = damage;
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
        public static readonly int CannonBallSize = 25;
        public CannonBall(Vector2 position, Vector2 direction, Texture2D texture, int damage)
        {
            Position = position;
            Direction = direction;
            Size = CannonBallSize;
            Texture = texture;
            PhysDamage = damage;
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
        public static readonly int MagicProjectileSize = 50;
        public MagicProjectile(Vector2 position, Vector2 direction, Texture2D texture, int damage)
        {
            Position = position;
            Direction = direction;
            Size = MagicProjectileSize;
            Texture = texture;
            MagicDamage = damage;
        }

        public override void Update(List<Enemy> enemyList)
        {
            base.Update(enemyList);

            Position += Direction * Speed;
            Rotation = (float)Math.Atan2(Direction.Y, Direction.X);
        }
    }
}