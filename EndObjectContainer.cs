using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp5Game
{
    public class EndObjectContainer
    {
        private MenuObjects endScreenImage;
        private MenuObjects retryBtn;
        private MenuObjects QuitBtn;

        public EndObjectContainer()
        {
            Point ImageSize = new Point(800, 600);
            Point btnSize = new Point(140, 60);

            endScreenImage = new MenuObjects(Assets.EndScreen, ImageSize);
            endScreenImage.CenterElement(Globals.WindowSize.Y, Globals.WindowSize.X);

            retryBtn = new MenuObjects(Assets.PriceButton, btnSize);
            retryBtn.CenterElement(Globals.WindowSize.Y + 50, Globals.WindowSize.X);
            
            QuitBtn = new MenuObjects(Assets.PriceButton, btnSize);
            QuitBtn.CenterElement(Globals.WindowSize.Y + 250, Globals.WindowSize.X);
        }
        public void Update()
        {
            retryBtn.Update();
            QuitBtn.Update();
            if(retryBtn.IsClicked())
            {
                Game1.CurrentScene = new MapCreationScene();
            }
            
            if(QuitBtn.IsClicked())
            {
                Game1.CurrentScene = new IntroScene();
            }
        }
        public void Draw()
        {
            retryBtn.Draw();
            QuitBtn.Draw();
            endScreenImage.Draw();
        }
    }
}