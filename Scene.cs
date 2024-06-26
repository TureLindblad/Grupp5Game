﻿using Microsoft.Xna.Framework;
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
            Globals.SpriteBatch.Draw(Assets.BackgroundImage,
                new Rectangle(
                    0, 0, Globals.WindowSize.X, Globals.WindowSize.Y)
                , Color.White);

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
                Color.White);
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
        private readonly HighScore Leaderboard;

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

            Leaderboard = new HighScore();
            HighScore.LoadSavedData();
        }

        public override void Update(GameTime gameTime)
        {
            playButton.Update();
            if (playButton.IsClicked())
            {
                Game1.CurrentScene = new MapCreationScene();
                HighScore.CurrentPlayerName = playerName;
                //playerNames.Add(playerName);
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
            Globals.SpriteBatch.Draw(Assets.BackgroundImage,
                new Rectangle(
                0, 0, Globals.WindowSize.X, Globals.WindowSize.Y)
                , Color.White);

            frame.Draw();
            playButton.Draw();
            inputBox.Draw();
            Vector2 playerNameSize = nameFont.MeasureString(playerName);

            string titleText = "ENTER YOUR NAME";
            Vector2 titleSize = nameFont.MeasureString(titleText);

            float titleX = (Globals.WindowSize.X - titleSize.X) / 2;
            float titleY = (Globals.WindowSize.Y - titleSize.Y) / 2;

            Globals.SpriteBatch.DrawString(TitleFont, titleText, new Vector2(titleX - 20, titleY - 150), Color.White);

            float textX = (Globals.WindowSize.X - playerNameSize.X) / 2;
            float textY = (Globals.WindowSize.Y - playerNameSize.Y) / 2;

            Globals.SpriteBatch.DrawString(nameFont, playerName, new Vector2(textX, textY - 50), Color.White);

            Leaderboard.Draw();
        }
    }

    public class MapCreationScene : Scene
    {
        private bool UndoIsPressed;
        public Grid MapGrid { get; private set; }
        PlayMapObject playButtonMap;
        PlayMapObject undoButton;
        private MouseState lastMouseState;
        private MouseState currentMouseState;

        public MapCreationScene()
        {
            Point buttonSize = new Point(126, 120);
            playButtonMap = new PlayMapObject(Assets.PlayBtnMapScene, buttonSize);
            playButtonMap.TopRightCorner(Globals.WindowSize.Y - 300, Globals.WindowSize.X - 10);
            undoButton = new PlayMapObject(Assets.UndoButton, buttonSize);
            undoButton.TopRightCorner(Globals.WindowSize.Y - 500, Globals.WindowSize.X - 10);
            MapGrid = new Grid();
        }
        public override void Update(GameTime gameTime)
        {
            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            MapGrid.Update(gameTime);

            undoButton.Update();

            playButtonMap.Update();

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

            if ((playButton && canPlay) ||
            (playButtonMap.IsClicked() && canPlay && lastMouseState != currentMouseState))
            {
                Game1.CurrentScene = new PlayMapScene(MapGrid);
            }

            if ((!UndoIsPressed && Keyboard.GetState().IsKeyDown(Keys.U) && MapGrid.PathTileOrder.Count > 1) ||
                (undoButton.IsClicked() && lastMouseState != currentMouseState) && MapGrid.PathTileOrder.Count > 1)
            {
                UndoIsPressed = true;

                Tile tileToRemove = MapGrid.PathTileOrder.Last();

                MapGrid.Tiles[tileToRemove.IndexPosition.X, tileToRemove.IndexPosition.Y]
                    = new SandTile(tileToRemove.IndexPosition.X, tileToRemove.IndexPosition.Y);

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
                "Tiles: " + (MapGrid.MaxNumberOfPathTiles - MapGrid.NumberOfPathTiles),
                new Vector2(1050, Globals.WindowSize.Y - 55),
                Color.Black);

            playButtonMap.Draw();
            undoButton.Draw();
        }
    }

    public class PlayMapScene : Scene
    {

        public Overlay GameOverlay { get; }
        private readonly MapObjectContainer MapObjects;
        public TowerTypes? SelectedTowerToPlace { get; set; }
        public Grid MapGrid { get; private set; }
        public EnemySpawner Spawner { get; private set; }
        public List<Enemy> EnemyList { get; private set; }
        private Color fadeColor = Color.White;
        private Color fadeColor2 = Color.White;
        private Color fadeColor3 = Color.White;
        private bool hasPressed1Key = false;
        private KeyboardState LastKeyboardState { get; set; }
        private KeyboardState CurrentKeyboardState { get; set; }

        private MouseState LastMouseState { get; set; }
        private MouseState CurrentMouseState { get; set; }

        public static List<Projectile> Projectiles = new List<Projectile>();
        public List<Explosion> Explosions = new List<Explosion>();
        public List<MagicBolt> MagicBolts = new List<MagicBolt>();

        private BuildingTile SelectedTowerTile { get; set; }

        public PlayMapScene(Grid drawnGrid)
        {

            MapObjects = new MapObjectContainer();
            SelectedTowerToPlace = null;
            GameOverlay = new Overlay();
            MapGrid = drawnGrid;
            Spawner = new EnemySpawner();
            EnemyList = new List<Enemy>();
            SceneryCreation.CreateScenery(this);
        }

        public override void Update(GameTime gameTime)
        {
            LastMouseState = CurrentMouseState;
            CurrentMouseState = Mouse.GetState();


            LastKeyboardState = CurrentKeyboardState;
            CurrentKeyboardState = Keyboard.GetState();

            if (GameOverlay.NexusHealth <= 0)
            {
                Game1.CurrentScene = new EndScreenScene(GameOverlay);
            }

            Spawner.Update(this);

            MapGrid.Update(gameTime);

            for (int i = 0; i < EnemyList.Count; i++)
            {
                EnemyList[i].Update(this);

                if (EnemyList[i].MarkedForDeletion) EnemyList.Remove(EnemyList[i]);
            }

            for (int i = 0; i < Projectiles.Count; i++)
            {
                Projectiles[i].Update(EnemyList, this);
            }

            for (int i = 0; i < Explosions.Count; i++)
            {
                if (Explosions[i].MarkedForDeletion) Explosions.Remove(Explosions[i]);
            }

            for (int i = 0; i < MagicBolts.Count; i++)
            {
                if (MagicBolts[i].MarkedForDeletion) MagicBolts.Remove(MagicBolts[i]);
            }

            if (LastKeyboardState.IsKeyDown(Keys.D1) && CurrentKeyboardState.IsKeyUp(Keys.D1))
            {
                if (SelectedTowerToPlace == TowerTypes.Archer) SelectedTowerToPlace = null;
                else SelectedTowerToPlace = TowerTypes.Archer;
            }
            if (LastKeyboardState.IsKeyDown(Keys.D2) && CurrentKeyboardState.IsKeyUp(Keys.D2))
            {
                if (SelectedTowerToPlace == TowerTypes.Cannon) SelectedTowerToPlace = null;
                else SelectedTowerToPlace = TowerTypes.Cannon;
            }
            if (LastKeyboardState.IsKeyDown(Keys.D3) && CurrentKeyboardState.IsKeyUp(Keys.D3))
            {
                if (SelectedTowerToPlace == TowerTypes.Magic) SelectedTowerToPlace = null;
                else SelectedTowerToPlace = TowerTypes.Magic;
            }

            if (LastKeyboardState.IsKeyDown(Keys.D8) && CurrentKeyboardState.IsKeyUp(Keys.D8) &&
                SpecialAbilities.RainOfFireCooldown == 0)
            {
                SpecialAbilities.SpawnManyExplosions(this);
            }

            if (LastKeyboardState.IsKeyDown(Keys.D9) && CurrentKeyboardState.IsKeyUp(Keys.D9) &&
                SpecialAbilities.FrostNovaCooldown == 0)
            {
                SpecialAbilities.FreezeAllEnemies(this);

            }

            LookForSelectedTower(CurrentMouseState);

            if (Keyboard.GetState().IsKeyDown(Keys.D4) && SelectedTowerTile != null)
            {
                if (GameOverlay.PlayerGold >= 250)
                {
                    GameOverlay.PlayerGold -= 250;
                    SelectedTowerTile.UpgradingTower();
                    SelectedTowerTile = null;
                }
            }

            MapObjects.Update(this);
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

            foreach (Explosion explosion in Explosions)
            {
                explosion.Draw();
            }

            foreach (MagicBolt magicBolt in MagicBolts)
            {
                magicBolt.Draw();
            }

            if (SelectedTowerTile != null)
            {
                Globals.SpriteBatch.Draw(Assets.SandTexture, 
                    new Rectangle((int)SelectedTowerTile.TexturePosition.X - SelectedTowerTile.TextureResizeDimension / 2 + 16, 
                    (int)SelectedTowerTile.TexturePosition.Y - SelectedTowerTile.TextureResizeDimension / 2 + 13, 
                    SelectedTowerTile.TextureResizeDimension, 
                    SelectedTowerTile.TextureResizeDimension), 
                    Color.Orange * 0.5f);
            }

            GameOverlay.Draw();

            MapObjects.Draw(this);
        }

        public void RemoveProjectile(Projectile projectile)
        {
            if (projectile is CannonBall cannonBall)
            {
                Explosions.Add(new Explosion(projectile.Position, projectile.PhysDamage, cannonBall.SplashDiameter, this));
            }
            if (projectile is MagicProjectile)
            {
                MagicBolts.Add(new MagicBolt(projectile.Position, projectile.MagicDamage, 100, this));
            }

            Projectiles.Remove(projectile);
        }

        public void LookForSelectedTower(MouseState mouseState)
        {
            Vector2 mousePosition = mouseState.Position.ToVector2();

            for (int x = 0; x < MapGrid.Tiles.GetLength(0); x++)
            {
                for (int y = 0; y < MapGrid.Tiles.GetLength(1); y++)
                {
                    float distance = Vector2.Distance(mousePosition, MapGrid.Tiles[x, y].TexturePosition);
                    if (distance < MapGrid.Tiles[x, y].TextureResizeDimension / 2 && 
                        CurrentMouseState.LeftButton == ButtonState.Pressed &&
                        LastMouseState.LeftButton == ButtonState.Released)
                    {
                        if (MapGrid.Tiles[x, y] is BuildingTile buildingTile) SelectedTowerTile = buildingTile;
                        else if (SelectedTowerTile != null) SelectedTowerTile = null;
                        return;
                    }
                }
            }
        }
    }
    public class EndScreenScene : Scene
    {
        private readonly EndObjectContainer EndObjects;
        public EndScreenScene(Overlay overlay)
        {
            int finalScore = overlay.CurrentWave * overlay.EnemiesKilled + overlay.PlayerGold;
            HighScore.SaveData(Tuple.Create(HighScore.CurrentPlayerName, finalScore));
            EndObjects = new EndObjectContainer();
        }

        public override void Update(GameTime gameTime)
        {
            EndObjects.Update();
        }

        public override void Draw()
        {
            Globals.SpriteBatch.Draw(Assets.BackgroundImage,
                new Rectangle(
                0, 0, Globals.WindowSize.X, Globals.WindowSize.Y)
                , Color.White);

            EndObjects.Draw();
        }
    }
}