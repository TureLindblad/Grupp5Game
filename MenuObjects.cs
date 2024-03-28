using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Grupp5Game
{
    public class MenuObjects
    {
        private Texture2D texture;
        private Rectangle rect;
        private Point size;

        public MenuObjects(Texture2D texture, Point size)
        {
            this.size = size;
            this.texture = texture;
            this.rect = new Rectangle(0, 0, size.X, size.Y);
        }

        public void Update(StartScreenScene startScreenScene)
        {
        }

        public void Draw(StartScreenScene startScreenScene)
        {
            Globals.SpriteBatch.Draw(texture, rect, Color.White);
        }

        public void CenterElement(int windowHeight, int windowWidth)
        {
            int elementX = (windowWidth - rect.Width) / 2;
            int elementY = (windowHeight - rect.Height) / 2;
            rect = new Rectangle(elementX, elementY, rect.Width, rect.Height);
        }
        public bool IsClicked()
        {
            return rect.Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y)) &&
                Mouse.GetState().LeftButton == ButtonState.Pressed;
        }
    }
}