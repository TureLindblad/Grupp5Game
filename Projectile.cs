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

        public abstract Task ApplyProjectileEffect(Enemy enemy);

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
            Damage = damage;
        }

        public async override Task ApplyProjectileEffect(Enemy enemy)
        {
            await Task.CompletedTask;
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
            Damage = damage;
        }

        public async override Task ApplyProjectileEffect(Enemy enemy)
        {
            Task fireDOT = Task.Delay(3000);

            while (!fireDOT.IsCompleted)
            {
                enemy.HealthBar.CurrentHealth -= enemy.HealthBar.MaxHealth * 0.1f;
                enemy.TextureColor = Color.Red;

                await Task.Delay(500);

                enemy.TextureColor = Color.White;
            }
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
            Damage = damage;
        }

        public async override Task ApplyProjectileEffect(Enemy enemy)
        {
            if (enemy.SpeedMod == 0)
            {
                enemy.SpeedMod = enemy.Speed / 2;
                enemy.Speed = enemy.SpeedMod;
            }
            enemy.TextureColor = Color.Blue;

            await Task.Delay(1000);

            if (enemy.SpeedMod != 0)
            {
                enemy.Speed *= 2;
                enemy.SpeedMod = 0;
            }
            
            enemy.TextureColor = Color.White;
        }

        public override void Update(List<Enemy> enemyList)
        {
            base.Update(enemyList);

            Position += Direction * Speed;
            Rotation = (float)Math.Atan2(Direction.Y, Direction.X);
        }
    }
}