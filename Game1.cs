﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Grupp5Game
{
    public static class Globals
    {
        public static Point WindowSize = new Point(1600, 900);
        public static Point MapDimensions = new Point(25, 11);
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

            Globals.Graphics.IsFullScreen = false;
        }

        protected override void Initialize()
        {
            CurrentScene = new IntroScene();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Globals.SpriteBatch = new SpriteBatch(GraphicsDevice);

            Assets.Overlay = Content.Load<Texture2D>("Overlay");
            Assets.TowerTexture = Content.Load<Texture2D>("TowerHex");
            Assets.GrassTexture = Content.Load<Texture2D>("Sprites/GrassHex");
            Assets.SandTexture = Content.Load<Texture2D>("Sprites/SandHex");
            Assets.TowerTexture = Content.Load<Texture2D>("TowerHex");
            Assets.NexusTexture = Content.Load<Texture2D>("Sprites/NexusHex");
            Assets.NexusTextureOuter = Content.Load<Texture2D>("Sprites/NexusHexOuter");
            Assets.EnemyGoblinTexture = Content.Load<Texture2D>("Sprites/EnemyGoblin");
            Assets.FrostEnemyTexture = Content.Load<Texture2D>("Sprites/FrostEnemy");
            Assets.IntroTextTexture = Content.Load<Texture2D>("TextSprites/IntroText");
            Assets.IntroTextFont = Content.Load<SpriteFont>("Text/IntroText");
            Assets.PlayButton = Content.Load<Texture2D>("Buttons/play_btn");
            Assets.Frame = Content.Load<Texture2D>("Buttons/frame");
            Assets.PlayerName = Content.Load<SpriteFont>("Text/playername");
            Assets.Title = Content.Load<SpriteFont>("Text/title");
            Assets.NameBox = Content.Load<Texture2D>("Buttons/name");
            Assets.MapCreationFont = Content.Load<SpriteFont>("Text/MapCreation");
            Assets.TowerBuildingTexture = Content.Load<Texture2D>("Sprites/TowerBuildtile");
            Assets.FireEnemyTexture = Content.Load<Texture2D>("Sprites/FireElemental");
            Assets.FlyingEnemyTexture = Content.Load<Texture2D>("Sprites/FlyingEnemy");
            Assets.BasetowerTexture = Content.Load<Texture2D>("Sprites/Basetower");
            Assets.FastEnemyTexture = Content.Load<Texture2D>("Sprites/FastEnemy");
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

            CurrentScene.Update(gameTime);

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
