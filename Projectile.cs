using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TD_shooter
{
    public class Arrow
    {
        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }
        public float Speed { get; set; }
        public Texture2D Texture { get; set; }
        public int Damage { get; set; }
        public float Rotation { get; set; }

        public Arrow(Vector2 position, Vector2 direction, Texture2D texture, int damage)
        {
            Position = position;
            Direction = direction;
            Speed = 15f;
            Texture = texture;
            Damage = damage;
        }

        public void Update()
        {
            Position += Direction * Speed;
            Rotation = (float)Math.Atan2(Direction.Y, Direction.X);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, Rotation, new Vector2(Texture.Width / 2, Texture.Height / 2), 1.0f, SpriteEffects.None, 0);
        }
    }

    public class CannonBall
    {
        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }
        public float Speed { get; set; }
        public Texture2D Texture { get; set; }
        public int Damage { get; set; }

        public CannonBall(Vector2 position, Vector2 direction, Texture2D texture, int damage)
        {
            Position = position;
            Direction = direction;
            Speed = 5f;
            Texture = texture;
            Damage = damage;
        }

        public void Update()
        {
            Position += Direction * Speed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }

    public class MagicProjectile
    {
        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }
        public float Speed { get; set; }
        public Texture2D Texture { get; set; }
        public int Damage { get; set; }
        public float Rotation { get; set; }

        public MagicProjectile(Vector2 position, Vector2 direction, Texture2D texture, int damage)
        {
            Position = position;
            Direction = direction;
            Speed = 9f;
            Texture = texture;
            Damage = damage;
        }

        public void Update()
        {
            Position += Direction * Speed;
            Rotation = (float)Math.Atan2(Direction.Y, Direction.X);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, Rotation, new Vector2(Texture.Width / 2, Texture.Height / 2), 1.0f, SpriteEffects.None, 0);
        }
    }
}