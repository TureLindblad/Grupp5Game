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
        public int CurrentFrame { get; set; }
        public int FrameCount { get; private set; }
        public int FrameHeight { get { return Texture.Height;  } }
    
    
        public int FrameWidth { get { return Texture.Width / FrameCount; } }
        public float FrameSpeed { get; set; }
        public bool IsLooping { get; set; }
        public Texture2D Texture { get; private set; }



        public Animation(Texture2D texture, int frameCount)
        {
            Texture = texture;
            FrameCount = frameCount;
            IsLooping = true;
            FrameSpeed = 0.2f;
        }

        public Rectangle GetNextAtlasFrame()
        {

            return new Rectangle(CurrentFrame * FrameWidth,
                          0,
                          FrameWidth,
                          FrameHeight);
        }

        private Rectangle[] walkingAnimationFrames = new Rectangle[]
        {

            new Rectangle(0, 0, 64, 64), // Första kolumnen i första raden, antag 64x64 dimensioner för exempeländamål
            new Rectangle(64, 0, 64, 64), // Andra kolumnen i första raden
            new Rectangle(128, 0, 64, 64), // Tredje kolumnen i första raden
            new Rectangle(0, 64, 64, 64), // Första kolumnen i andra raden
            new Rectangle(64, 64, 64, 64), // Andra kolumnen i andra raden
            new Rectangle(128, 64, 64, 64) // Tredje kolumnen i andra raden
        };

    }
}
