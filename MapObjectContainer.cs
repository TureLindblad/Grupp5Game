using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp5Game
{
    public class MapObjectContainer
    {
        private PlayMapObject archer;
        private PlayMapObject magic;
        private PlayMapObject artillery;
        private PlayMapObject artilleryFrame;
        private PlayMapObject magicFrame;
        private PlayMapObject archerFrame;
        private PlayMapObject archerButton;
        private PlayMapObject artilleryButton;
        private PlayMapObject magicButton;
        private PlayMapObject upgradeButton;

        public MapObjectContainer()
        {
            Point size = new Point(126, 120);
            Point frameSize = new Point(140, 132);
            Point buttonSize = new Point(140, 51);
            archer = new PlayMapObject(Assets.Archer, size);
            magic = new PlayMapObject(Assets.Magic, size);
            artillery = new PlayMapObject(Assets.Artillery, size);

            archerFrame = new PlayMapObject(Assets.Frame, frameSize);
            artilleryFrame = new PlayMapObject(Assets.Frame, frameSize);
            magicFrame = new PlayMapObject(Assets.Frame, frameSize);

            archerButton = new PlayMapObject(Assets.PriceButton, buttonSize);
            artilleryButton = new PlayMapObject(Assets.PriceButton, buttonSize);
            magicButton = new PlayMapObject(Assets.PriceButton, buttonSize);

            upgradeButton = new PlayMapObject(Assets.UpgradeButton, new Point(155, 65));

            archer.TopRightCorner(Globals.WindowSize.Y - 750, Globals.WindowSize.X - 20);
            archerFrame.TopRightCorner(Globals.WindowSize.Y - 745, Globals.WindowSize.X - 10);
            archerButton.TopRightCorner(Globals.WindowSize.Y - 690, Globals.WindowSize.X - 10);

            magic.TopRightCorner(Globals.WindowSize.Y - 350, Globals.WindowSize.X - 20);
            magicFrame.TopRightCorner(Globals.WindowSize.Y - 345, Globals.WindowSize.X - 10);
            magicButton.TopRightCorner(Globals.WindowSize.Y - 290, Globals.WindowSize.X - 10);

            artillery.TopRightCorner(Globals.WindowSize.Y - 550, Globals.WindowSize.X - 20);
            artilleryFrame.TopRightCorner(Globals.WindowSize.Y - 545, Globals.WindowSize.X - 10);
            artilleryButton.TopRightCorner(Globals.WindowSize.Y - 490, Globals.WindowSize.X - 10);

            upgradeButton.TopRightCorner(Globals.WindowSize.Y - 75, Globals.WindowSize.X);
        }
        public void Update(PlayMapScene mapScene)
        {
            archerFrame.Color = Color.White;
            artilleryFrame.Color = Color.White;
            magicFrame.Color = Color.White;

            switch (mapScene.SelectedTowerToPlace)
            {
                case TowerTypes.Archer:
                    archerFrame.Color = Color.Red;
                    break;
                case TowerTypes.Cannon:
                    artilleryFrame.Color = Color.Red;
                    break;
                case TowerTypes.Magic:
                    magicFrame.Color = Color.Red;
                    break;
            }
        }

        public void Draw(PlayMapScene mapScene)
        {
            archer.Draw(mapScene);
            magic.Draw(mapScene);
            artillery.Draw(mapScene);

            artilleryFrame.Draw(mapScene);
            magicFrame.Draw(mapScene);
            archerFrame.Draw(mapScene);

            archerButton.Draw(mapScene);
            artilleryButton.Draw(mapScene);
            magicButton.Draw(mapScene);

            upgradeButton.Draw(mapScene);
        }
    }
}
