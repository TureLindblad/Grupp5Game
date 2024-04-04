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
    public enum TowerTypes
    {
        Archer, Cannon, Magic
    }
    public abstract class Scene
    {
        public abstract void Update(GameTime gameTime);
        public abstract void Draw();
    }

    public class IntroScene : Scene
    {
        private static float IntroTextResize = Math.Min(
            Globals.WindowSize.X / (float)Assets.IntroTextTexture.Width,
            Globals.WindowSize.Y / (float)Assets.IntroTextTexture.Height);

        private static int IntroTextWidth = (int)(Assets.IntroTextTexture.Width * IntroTextResize * 0.8);
        private static int IntroTextHeight = (int)(Assets.IntroTextTexture.Height * IntroTextResize * 0.8);

        public override void Update(GameTime gameTime)
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
        MenuObjects nameBox;
        List<string> playerNames = new List<string>();
        string playerName = "";
        bool keyAlreadyPressed = false;
        SpriteFont nameFont;
        SpriteFont TitleFont;

        public StartScreenScene()
        {
            Point frameSize = new Point(450, 450);
            Point buttonSize = new Point(150, 80);
            Point nameBoxSize = new Point(200, 200);
            nameFont = Assets.PlayerName;
            TitleFont = Assets.Title;
            nameBox = new MenuObjects(Assets.NameBox, nameBoxSize);
            frame = new MenuObjects(Assets.Frame, frameSize);
            playButton = new MenuObjects(Assets.PlayButton, buttonSize);

            frame.CenterElement(Globals.WindowSize.Y, Globals.WindowSize.X);
            playButton.CenterElement(Globals.WindowSize.Y + 200, Globals.WindowSize.X);
            nameBox.CenterElement(Globals.WindowSize.Y - 100, Globals.WindowSize.X);

        }

        public override void Update(GameTime gameTime)
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
            nameBox.Draw(this);
            Vector2 playerNameSize = nameFont.MeasureString(playerName);

            string titleText = "ENTER YOUR NAME";
            Vector2 titleSize = nameFont.MeasureString(titleText);

            float titleX = (Globals.WindowSize.X - titleSize.X) / 2;
            float titleY = (Globals.WindowSize.Y - titleSize.Y) / 2;

            Globals.SpriteBatch.DrawString(TitleFont, titleText, new Vector2(titleX - 50, titleY - 150), Color.Black);

            float textX = (Globals.WindowSize.X - playerNameSize.X) / 2;
            float textY = (Globals.WindowSize.Y - playerNameSize.Y) / 2;

            Globals.SpriteBatch.DrawString(nameFont, playerName, new Vector2(textX, textY - 50), Color.Black);
        }
    }

    public class MapCreationScene : Scene
    {
        private bool UndoIsPressed;
        public Grid MapGrid { get; private set; }

        public MapCreationScene()
        {
            MapGrid = new Grid();
        }
        public override void Update(GameTime gameTime)
        {
            MapGrid.Update(gameTime);

            bool playButton = Keyboard.GetState().IsKeyDown(Keys.P);
            bool canPlay = false;

            foreach (var nexus in MapGrid.OuterNexusTiles)
            {
                int numberOfAdjacentPaths = MapGrid.GetNeighborTiles(nexus.Key).Count;

                if (numberOfAdjacentPaths == 1) 
                {
                    canPlay = true;
                }
                else if (numberOfAdjacentPaths > 1)
                {
                    canPlay = false;
                    break;
                }
            }

            if (playButton && canPlay) Game1.CurrentScene = new PlayMapScene(MapGrid);

            if (!UndoIsPressed && Keyboard.GetState().IsKeyDown(Keys.U) && MapGrid.PathTileOrder.Count > 1) 
            {
                UndoIsPressed = true;

                Tile tileToRemove = MapGrid.PathTileOrder.Last();

                MapGrid.Tiles[tileToRemove.IndexPosition.X, tileToRemove.IndexPosition.Y]
                    = new GrassTile(tileToRemove.IndexPosition.X, tileToRemove.IndexPosition.Y);

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

            Globals.SpriteBatch.Draw(
                Assets.Overlay,
                new Rectangle(0, 0, Globals.WindowSize.X, Globals.WindowSize.Y),
                null,
                Color.White);

            Globals.SpriteBatch.DrawString(
                Assets.IntroTextFont,
                "UNDO: U. Tiles: " + (MapGrid.MaxNumberOfPathTiles - MapGrid.NumberOfPathTiles),
                new Vector2(1050, Globals.WindowSize.Y - 55),
                Color.Black);
        }
    }

    public class PlayMapScene : Scene
    {
        public Overlay GameOverlay { get; }
        public TowerTypes SelectedTowerToPlace { get; set; }
        public Grid MapGrid { get; private set; }
        public EnemySpawner Spawner { get; private set; }
        public List<Enemy> EnemyList { get; private set; }

        public static List<Projectile> Projectiles = new List<Projectile>();
        public PlayMapScene(Grid drawnGrid)
        {
            SelectedTowerToPlace = TowerTypes.Archer;
            GameOverlay = new Overlay(); 
            MapGrid = drawnGrid;
            Spawner = new EnemySpawner();
            EnemyList = new List<Enemy>();
        }

        public override void Update(GameTime gameTime)
        {
            /*if (GameOverlay.NexusHealth <= 0)
            {
                Game1.CurrentScene = new EndScreenScene();
            }*/

            Spawner.Update(this);

            MapGrid.Update(gameTime);

            for (int i = 0; i < EnemyList.Count; i++)
            {
                EnemyList[i].Update(this);

                if (EnemyList[i].MarkedForDeletion) EnemyList.Remove(EnemyList[i]);
            }

            for (int i = 0; i < Projectiles.Count; i++)
            {
                Projectiles[i].Update(EnemyList);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D1)) SelectedTowerToPlace = TowerTypes.Archer;
            if (Keyboard.GetState().IsKeyDown(Keys.D2)) SelectedTowerToPlace = TowerTypes.Cannon;
            if (Keyboard.GetState().IsKeyDown(Keys.D3)) SelectedTowerToPlace = TowerTypes.Magic;
        }

        public override void Draw()
        {
            MapGrid.Draw();

            foreach (Enemy enemy in EnemyList)
            {
                enemy.Draw(this);
            }

            foreach (Projectile projectile in Projectiles)
            {
                projectile.Draw();
            }

            GameOverlay.Draw();
        }
    }

    public class EndScreenScene() : Scene
    {
        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw()
        {

        }
    }
}
