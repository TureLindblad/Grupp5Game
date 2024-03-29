using Microsoft.Xna.Framework;

namespace Grupp5Game
{
    public class HealthBar
    {
        private float healthBarWidth;
        public float MaxHealth { get; set; }
        public float CurrentHealth { get; set; }
        public HealthBar(int health)
        {
            healthBarWidth = 1;
            MaxHealth = health;
            CurrentHealth = health;
        }
        public void Update()
        {
            healthBarWidth = CurrentHealth / MaxHealth ;
        }
        public void Draw(Vector2 position, int size)
        {
            Rectangle healthBarRect = new Rectangle(
                (int)position.X - size / 2 + 5,
                (int)position.Y - size / 2 - 20,
                (int)(healthBarWidth * size),
                20);

            Rectangle healthBarRectEmpty = new Rectangle(
                (int)position.X - size / 2 + 5,
                (int)position.Y - size / 2 - 20,
                size,
                20);

            Globals.SpriteBatch.Draw(
                texture: Assets.NameBox,
                destinationRectangle: healthBarRectEmpty,
                color: Color.Red);

            Globals.SpriteBatch.Draw(
                texture: Assets.NameBox,
                destinationRectangle: healthBarRect,
                color: Color.White);

        }
    }
}
