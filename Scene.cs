using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp5Game
{
    public abstract class Scene
    {
        public abstract void Update();
        public abstract void Draw();
    }

    public class IntroScene : Scene
    {
        private static float IntroTextResize = Math.Min(
            Globals.WindowSize.X / (float)Assets.IntroTextTexture.Width,
            Globals.WindowSize.Y / (float)Assets.IntroTextTexture.Height);

        private static int IntroTextWidth = (int)(Assets.IntroTextTexture.Width * IntroTextResize * 0.8);
        private static int IntroTextHeight = (int)(Assets.IntroTextTexture.Height * IntroTextResize * 0.8);

        public override void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space)) Game1.CurrentScene = new StartScreenScene();
        }

        public override void Draw()
        {
            Globals.SpriteBatch.Draw(Assets.IntroTextTexture,
                new Rectangle(
                    Globals.WindowSize.X / 2 - IntroTextWidth / 2,
                    Globals.WindowSize.Y / 2 - IntroTextHeight / 2,
                    IntroTextWidth,
                    IntroTextHeight),
                Color.White);

            Globals.SpriteBatch.DrawString(
                Assets.IntroTextFont,
                "PRESS SPACE TO CONTINUE",
                new Vector2(Globals.WindowSize.X / 2 - 280, Globals.WindowSize.Y - 90),
                Color.Black);
        }
    }

    public class StartScreenScene : Scene
    {
        MenuObjects frame;
        MenuObjects playButton;
        MenuObjects inputBox;
        List<string> playerNames = new List<string>();
        string playerName = "";
        bool keyAlreadyPressed = false;
        SpriteFont nameFont;
        SpriteFont TitleFont;

        public StartScreenScene()
        {
            Point frameSize = new Point(450, 450);
            Point buttonSize = new Point(200, 80);
            Point inputBoxSize = new Point(300, 72);
            nameFont = Assets.PlayerName;
            TitleFont = Assets.Title;
            inputBox = new MenuObjects(Assets.InputBox, inputBoxSize);
            frame = new MenuObjects(Assets.Frame, frameSize);
            playButton = new MenuObjects(Assets.PlayButton, buttonSize);

            frame.CenterElement(Globals.WindowSize.Y, Globals.WindowSize.X);
            playButton.CenterElement(Globals.WindowSize.Y + 200, Globals.WindowSize.X);
            inputBox.CenterElement(Globals.WindowSize.Y - 100, Globals.WindowSize.X);

        }

        public override void Update()
        {
            playButton.Update(this);
            if (playButton.IsClicked())
            {
                Game1.CurrentScene = new MapCreationScene();
                playerNames.Add(playerName);
            }
            KeyboardState keyboardState = Keyboard.GetState();
            Keys[] pressedKeys = keyboardState.GetPressedKeys();
            foreach (Keys key in pressedKeys)
            {
                if (playerName.Length <= 10)
                {
                    if (key >= Keys.A && key <= Keys.Z && !keyAlreadyPressed)
                    {
                        playerName += key.ToString();
                        keyAlreadyPressed = true;
                    }
                    else if (key == Keys.Back && playerName.Length > 0)
                    {
                        playerName = playerName.Remove(playerName.Length - 1);
                    }
                }
            }
            if (pressedKeys.Length == 0)
            {
                keyAlreadyPressed = false;
            }
        }

        public override void Draw()
        {
            frame.Draw(this);
            playButton.Draw(this);
            inputBox.Draw(this);
            Vector2 playerNameSize = nameFont.MeasureString(playerName);

            string titleText = "ENTER YOUR NAME";
            Vector2 titleSize = nameFont.MeasureString(titleText);

            float titleX = (Globals.WindowSize.X - titleSize.X) / 2;
            float titleY = (Globals.WindowSize.Y - titleSize.Y) / 2;

            Globals.SpriteBatch.DrawString(TitleFont, titleText, new Vector2(titleX - 20, titleY - 150), Color.Black);

            float textX = (Globals.WindowSize.X - playerNameSize.X) / 2;
            float textY = (Globals.WindowSize.Y - playerNameSize.Y) / 2;

            Globals.SpriteBatch.DrawString(nameFont, playerName, new Vector2(textX, textY - 50), Color.White);
        }
    }

    public class MapCreationScene : Scene
    {
        private bool UndoIsPressed;
        public Grid MapGrid { get; private set; }

        public MapCreationScene()
        {
            MapGrid = new Grid(false);
        }
        public override void Update()
        {
            MapGrid.Update();

            if (Keyboard.GetState().IsKeyDown(Keys.P) &&
                MapGrid.GetNeighborTiles(MapGrid.Tiles[Grid.NexusIndex.X, Grid.NexusIndex.Y]).Count == 1)
            {
                Game1.CurrentScene = new PlayMapScene(MapGrid);
            }

            if (!UndoIsPressed && Keyboard.GetState().IsKeyDown(Keys.U) && MapGrid.PathTileOrder.Count > 1)
            {
                UndoIsPressed = true;

                Tile tileToRemove = MapGrid.PathTileOrder.Last();

                MapGrid.Tiles[tileToRemove.IndexPosition.X, tileToRemove.IndexPosition.Y]
                    = new TerrainTile(tileToRemove.IndexPosition.X, tileToRemove.IndexPosition.Y);

                MapGrid.PathTileOrder.Remove(MapGrid.PathTileOrder.Last());

                MapGrid.NumberOfPathTiles--;
            }

            if (Keyboard.GetState().IsKeyUp(Keys.U))
            {
                UndoIsPressed = false;
            }
        }

        public override void Draw()
        {
            MapGrid.Draw();

            Globals.SpriteBatch.DrawString(
                Assets.IntroTextFont,
                "UNDO PRESS: U. Number of tiles left: " + (MapGrid.MaxNumberOfPathTiles - MapGrid.NumberOfPathTiles),
                new Vector2(Globals.WindowSize.X / 2 - 350, Globals.WindowSize.Y - 90),
                Color.Black);
        }
    }

    public class PlayMapScene : Scene
    {
        public Grid MapGrid { get; private set; }
        public EnemySpawner Spawner { get; private set; }
        public List<Enemy> EnemyList { get; private set; }
        PlayMapObject archer;
        PlayMapObject magic;
        PlayMapObject artillery;
        PlayMapObject frame;
        PlayMapObject frame2;
        PlayMapObject frame3;
        PlayMapObject archerButton;
        PlayMapObject artilleryButton;
        PlayMapObject magicButton;
        PlayMapObject upgradeButton;

        public PlayMapScene(Grid drawnGrid)
        {
            Point Seize = new Point(126, 120);
            Point frameSeize = new Point(140, 132);
            Point buttonSeize = new Point(140, 51);
            archer = new PlayMapObject(Assets.Archer, Seize);
            magic = new PlayMapObject(Assets.Magic, Seize);
            artillery = new PlayMapObject(Assets.Artillery, Seize);

            frame = new PlayMapObject(Assets.Frame, frameSeize);
            frame2 = new PlayMapObject(Assets.Frame, frameSeize);
            frame3 = new PlayMapObject(Assets.Frame, frameSeize);

            archerButton = new PlayMapObject(Assets.PriceButton, buttonSeize);
            artilleryButton = new PlayMapObject(Assets.PriceButton, buttonSeize);
            magicButton = new PlayMapObject(Assets.PriceButton, buttonSeize);

            upgradeButton = new PlayMapObject(Assets.UpgradeButton, new Point(182, 80));

            MapGrid = drawnGrid;
            Spawner = new EnemySpawner();
            EnemyList = new List<Enemy>();

            archer.TopRightCorner(Globals.WindowSize.Y - 750, Globals.WindowSize.X);
            frame3.TopRightCorner(Globals.WindowSize.Y - 745, Globals.WindowSize.X + 10);
            archerButton.TopRightCorner(Globals.WindowSize.Y - 690, Globals.WindowSize.X + 10);

            magic.TopRightCorner(Globals.WindowSize.Y - 350, Globals.WindowSize.X);
            frame2.TopRightCorner(Globals.WindowSize.Y - 345, Globals.WindowSize.X + 10);
            magicButton.TopRightCorner(Globals.WindowSize.Y - 290, Globals.WindowSize.X + 10);

            artillery.TopRightCorner(Globals.WindowSize.Y - 550, Globals.WindowSize.X);
            frame.TopRightCorner(Globals.WindowSize.Y - 545, Globals.WindowSize.X + 10);
            artilleryButton.TopRightCorner(Globals.WindowSize.Y - 490, Globals.WindowSize.X + 10);

            upgradeButton.TopRightCorner(Globals.WindowSize.Y - 75, Globals.WindowSize.X + 20);
        }

        public override void Update()
        {
            Spawner.Update(this);

            MapGrid.Update();

            for (int i = 0; i < EnemyList.Count; i++)
            {
                EnemyList[i].Update(this);

                if (EnemyList[i].MarkedForDeletion) EnemyList.Remove(EnemyList[i]);
            }
        }

        public override void Draw()
        {
            archer.Draw(this);
            magic.Draw(this);
            artillery.Draw(this);

            frame.Draw(this);
            frame2.Draw(this);
            frame3.Draw(this);

            archerButton.Draw(this);
            artilleryButton.Draw(this);
            magicButton.Draw(this);

            upgradeButton.Draw(this);

            MapGrid.Draw();

            foreach (Enemy enemy in EnemyList)
            {
                enemy.Draw(this);
            }
        }
    }

    public class EndScreenScene() : Scene
    {
        public override void Update()
        {

        }

        public override void Draw()
        {

        }
    }
}
