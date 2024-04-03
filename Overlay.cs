using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp5Game
{
    public class Overlay
    {
        private readonly Dictionary<int, Texture2D> Hearts;
        public int NexusHealth { get; set; }
        public int PlayerGold { get; set; }
        public int EnemiesKilled { get; set; }
        public int CurrentWave { get; set; }


        public Overlay()
        {
            Hearts = new Dictionary<int, Texture2D>();
            PlayerGold = 1000;
            CurrentWave = 1;

            for (int i = 0; i < 10; i++)
            {
                Hearts[i] = Assets.FullHeart;
            }

            NexusHealth = 10;
        }

        public void SubtractHeart()
        {
            NexusHealth--;

            Hearts[NexusHealth] = Assets.EmptyHeart;
        }

        public void Draw()
        {
            int xMod = 0;

            Globals.SpriteBatch.Draw(
                Assets.Overlay,
                new Rectangle(0, 0, Globals.WindowSize.X, Globals.WindowSize.Y),
                null,
                Color.White);

            foreach (var kv in  Hearts)
            {
                Globals.SpriteBatch.Draw(kv.Value, new Rectangle(1020 + xMod, Globals.WindowSize.Y - 50, 34, 34), Color.White);
                xMod += 37;
            }

            Globals.SpriteBatch.DrawString(
                Assets.IntroTextFont,
                $"{PlayerGold}",
                new Vector2(110, Globals.WindowSize.Y - 55),
                Color.Black);

            Globals.SpriteBatch.DrawString(
                Assets.IntroTextFont,
                $"{CurrentWave}",
                new Vector2(445, Globals.WindowSize.Y - 55),
                Color.Black);

            Globals.SpriteBatch.DrawString(
                Assets.IntroTextFont,
                $"{EnemiesKilled}",
                new Vector2(730, Globals.WindowSize.Y - 55),
                Color.Black);
        }
    }
}
