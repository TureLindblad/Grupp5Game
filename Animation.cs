using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp5Game
{
    public class Animation
    {
        private int CurrentFrame { get; set; }
        public int FrameCount { get; private set; }
        public int FrameHeight { get { return Texture.Height; } }
        public int FrameWidth { get { return Texture.Width / FrameCount; } }
        public float FrameSpeed { get; set; }
        public bool IsLooping { get; set; }
        public Texture2D Texture { get; private set; }
        private Rectangle[] rectangles;
        private float elapsedTime = 0f;

        public Animation(Texture2D texture, int frameCount)
        {
            Texture = texture;
            FrameCount = frameCount;
            IsLooping = true;
            FrameSpeed = 0.05f;
            GetRectangles();
        }

        

        public void Draw(Rectangle destination)
        {
            
            elapsedTime += (float)Globals.GameTime.ElapsedGameTime.TotalSeconds;

            
            if (elapsedTime >= FrameSpeed)
            {
                CurrentFrame++;
                if (CurrentFrame >= FrameCount)
                {
                    if (IsLooping)
                        CurrentFrame = 0;
                    else
                        CurrentFrame = FrameCount - 1; 
                }

                
                elapsedTime = 0f;
            }
            Globals.SpriteBatch.Draw(Texture, destination, rectangles[CurrentFrame], Color.White);

        }

        private void GetRectangles()
        {
            rectangles = new Rectangle[FrameCount];
            for (int i = 0; i < FrameCount; i++)
            {
                rectangles[i] = new Rectangle(i * FrameWidth, 0, FrameWidth, FrameHeight);
            }
        }
    }
}
