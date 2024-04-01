using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Grupp5Game
{
    public class PlayMapObject
    {
        private Texture2D texture;
        private Rectangle rect;
        private Point size;

        public PlayMapObject(Texture2D texture, Point size)
        {
            this.size = size;
            this.texture = texture;
            this.rect = new Rectangle(0, 0, size.X, size.Y);
        }
        public void Update(PlayMapScene playMapScene)
        {

        }
        public void Draw(PlayMapScene playMapScene)
        {
            Globals.SpriteBatch.Draw(texture, rect, Color.White);
        }
        public void TopRightCorner(int windowHeight, int windowWidth)
        {
            int elementX = windowWidth - rect.Width;
            int elementY = windowHeight - rect.Height;
            rect = new Rectangle(elementX, elementY, rect.Width, rect.Height);
        }

    }
}