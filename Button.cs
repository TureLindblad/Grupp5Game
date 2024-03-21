using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Grupp5Game
{
    public class Button
    {
        private Texture2D Texture;
        public int Size { get; set; }
        public Vector2 Position { get; set; }
        private Rectangle rect;
        private Color shade = Color.White;

        public Button()
        {
            Texture = Assets.PlayButton;
            Position = new(100, 100);
            Size = 500;
            rect = new((int)Position.X, (int)Position.Y, Size, Size);
        }

        public void Update(StartScreenScene startScreenScene)
        {
            MouseState mouseState = Mouse.GetState();
            Rectangle cursor = new Rectangle(mouseState.X, mouseState.Y, 1, 1);

            if (cursor.Intersects(rect))
            {
                shade = Color.DarkGray;
                if(mouseState.LeftButton == ButtonState.Pressed)
                {
                    Game1.CurrentScene = new MapScene();
                }
            }
            else
            {
                shade = Color.White;
            }
        }
        public void Draw(StartScreenScene startScreenScene)
        {
            Globals.SpriteBatch.Draw(Texture, new Rectangle((int)Position.X, (int)Position.Y, Size, Size), shade);
        }
    }
}
