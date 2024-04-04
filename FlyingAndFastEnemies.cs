using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Grupp5Game
{
    public class FlyingAndFastEnemies : Enemy
    {
        public FlyingAndFastEnemies(Texture2D texture) : base(texture)
        {

        }
    }
    public class FlyingEnemy : FlyingAndFastEnemies
    {
        public FlyingEnemy(Texture2D texture) : base(texture)
        {
            HealthBar = new HealthBar(15);
            Size = 30;
            Speed = 5;
            AttacksTower = false;
        }

        public override void Draw(PlayMapScene mapScene)
        {
            HealthBar.Draw(new Vector2(Position.X, Position.Y - 50), Size);

            Globals.SpriteBatch.Draw(
                    Texture,
                    new Rectangle(
                    (int)Position.X - Size / 2 + 5, //5 hjälper men vet inte varför den behövs 
                    (int)Position.Y - Size / 2,
                    Size,
                    Size),
                    Color.White);
        }
    }
    public class FastEnemy : FlyingAndFastEnemies
    {
        public FastEnemy(Texture2D texture) : base(texture)
        {
            HealthBar = new HealthBar(15);
            Size = 30;
            Speed = 6;
            AttacksTower = false;
        }
    }
}
