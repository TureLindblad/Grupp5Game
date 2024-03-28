using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Grupp5Game
{
    public static class Globals
    {
        public static Point WindowSize = new Point(1600, 1000);
        public static Point MapDimensions = new Point(25, 10);
        public static GraphicsDeviceManager Graphics { get; set; }
        public static SpriteBatch SpriteBatch { get; set; }
    }

    public class Game1 : Game
    {
        public static Scene CurrentScene { get; set; }

        public Game1()
        {
            Globals.Graphics = new GraphicsDeviceManager(this);
            Globals.Graphics.PreferredBackBufferWidth = Globals.WindowSize.X;
            Globals.Graphics.PreferredBackBufferHeight = Globals.WindowSize.Y;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            CurrentScene = new IntroScene();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Globals.SpriteBatch = new SpriteBatch(GraphicsDevice);

            
            Assets.TowerTexture = Content.Load<Texture2D>("TowerHex");
            Assets.GrassTexture = Content.Load<Texture2D>("Sprites/GrassHex");
            Assets.SandTexture = Content.Load<Texture2D>("Sprites/SandHex");
            Assets.TowerTexture = Content.Load<Texture2D>("TowerHex");
            Assets.NexusTexture = Content.Load<Texture2D>("Sprites/NexusHex");
            Assets.EnemyGoblinTexture = Content.Load<Texture2D>("Sprites/EnemyGoblin");
            Assets.FrostEnemyTexture = Content.Load<Texture2D>("Sprites/FrostEnemy");
            Assets.IntroTextTexture = Content.Load<Texture2D>("TextSprites/IntroText");
            Assets.IntroTextFont = Content.Load<SpriteFont>("Text/IntroText");
            Assets.MapCreationFont = Content.Load<SpriteFont>("Text/MapCreation");
            Assets.TowerBuildingTexture = Content.Load<Texture2D>("Sprites/TowerBuildtile");

        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

            CurrentScene.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGreen);

            Globals.SpriteBatch.Begin();

            CurrentScene.Draw();

            Globals.SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
