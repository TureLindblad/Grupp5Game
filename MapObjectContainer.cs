﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp5Game
{
    public class MapObjectContainer
    {
        private SpriteFont archerPrice;
        private SpriteFont cannonPrice;
        private SpriteFont magicPrice;
        private PlayMapObject archer;
        private PlayMapObject magic;
        private PlayMapObject artillery;
        private PlayMapObject artilleryFrame;
        private PlayMapObject magicFrame;
        private PlayMapObject archerFrame;
        private PlayMapObject rainOfFireFrame;
        private PlayMapObject frostNovaFrame;
        private PlayMapObject archerButton;
        private PlayMapObject artilleryButton;
        private PlayMapObject magicButton;
        private PlayMapObject upgradeButton;
        private PlayMapObject rainOfFire;
        private PlayMapObject frostNova;

        public static Point AbilitiesSize = new Point(101, 64);
        public static Point RainOfFirePosition = new Point(Globals.WindowSize.X - 30, Globals.WindowSize.Y - 220);
        public static Point FrostNovaPosition = new Point(Globals.WindowSize.X - 30, Globals.WindowSize.Y - 140);

        public MapObjectContainer()
        {
            Point size = new Point(126, 120);
            Point frameSize = new Point(140, 132);
            Point buttonSize = new Point(140, 51);

            archerPrice = Assets.Title;
            cannonPrice = Assets.Title;
            magicPrice = Assets.Title;
            
            archer = new PlayMapObject(Assets.Archer, size);
            magic = new PlayMapObject(Assets.Magic, size);
            artillery = new PlayMapObject(Assets.Artillery, size);

            archerFrame = new PlayMapObject(Assets.Frame, frameSize);
            artilleryFrame = new PlayMapObject(Assets.Frame, frameSize);
            magicFrame = new PlayMapObject(Assets.Frame, frameSize);
            rainOfFireFrame = new PlayMapObject(Assets.Frame, new Point(110, 71));
            frostNovaFrame = new PlayMapObject(Assets.Frame, new Point(110, 71));

            archer = new PlayMapObject(Assets.Archer, size);
            magic = new PlayMapObject(Assets.Magic, size);
            artillery = new PlayMapObject(Assets.Artillery, size);

            archerButton = new PlayMapObject(Assets.PriceButton, buttonSize);
            artilleryButton = new PlayMapObject(Assets.PriceButton, buttonSize);
            magicButton = new PlayMapObject(Assets.PriceButton, buttonSize);

            rainOfFire = new PlayMapObject(Assets.RainOfFire, AbilitiesSize);
            frostNova = new PlayMapObject(Assets.FrostNova, AbilitiesSize);

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

            frostNova.TopRightCorner(FrostNovaPosition.Y, FrostNovaPosition.X);
            frostNovaFrame.TopRightCorner(Globals.WindowSize.Y -135, Globals.WindowSize.X - 25);

            rainOfFire.TopRightCorner(RainOfFirePosition.Y, RainOfFirePosition.X);
            rainOfFireFrame.TopRightCorner(Globals.WindowSize.Y -215, Globals.WindowSize.X - 25);

            upgradeButton.TopRightCorner(Globals.WindowSize.Y - 25, Globals.WindowSize.X);
        }
        public void Update(PlayMapScene mapScene)
        {
            artilleryFrame.Color = Color.White;
            archerFrame.Color = Color.White;
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
            artilleryFrame.Draw();
            magicFrame.Draw();
            archerFrame.Draw();

            archer.Draw();
            magic.Draw();
            artillery.Draw();

            archerButton.Draw();
            artilleryButton.Draw();
            magicButton.Draw();

            rainOfFireFrame.Draw();
            rainOfFire.Draw();
            SpecialAbilities.DrawFireRainCooldown();

            frostNovaFrame.Draw();
            frostNova.Draw();
            SpecialAbilities.DrawFrostNovaCooldown();

            if (mapScene.GameOverlay.PlayerGold < 250)
            {
                upgradeButton.Draw(0.5f);
            }
            else
            {
                upgradeButton.Draw();
            }

            string archerCost = ArcherTower.TowerCost.ToString();
            Vector2 stringSize = archerPrice.MeasureString(archerCost);

            float stringSizeX = Globals.WindowSize.X - stringSize.X - 20;
            float stringSizeY = 20;

            Globals.SpriteBatch.DrawString(archerPrice, archerCost, new Vector2(stringSizeX - 35, stringSizeY + 147), Color.Black);

            string cannonCost = CannonTower.TowerCost.ToString();
            Vector2 stringSize2 = cannonPrice.MeasureString(archerCost);

            Globals.SpriteBatch.DrawString(cannonPrice, cannonCost, new Vector2(stringSizeX - 35, stringSizeY + 347), Color.Black);

            string magicCost = MagicTower.TowerCost.ToString();
            Vector2 stringSize3 = magicPrice.MeasureString(archerCost);

            Globals.SpriteBatch.DrawString(magicPrice, magicCost, new Vector2(stringSizeX - 35, stringSizeY + 547), Color.Black);
        }
    }
}
