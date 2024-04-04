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

            Globals.Graphics.IsFullScreen = true;
        }

        protected override void Initialize()
        {
            CurrentScene = new IntroScene();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Globals.SpriteBatch = new SpriteBatch(GraphicsDevice);

            Assets.Overlay = Content.Load<Texture2D>("Overlay/Overlay");
            Assets.FullHeart = Content.Load<Texture2D>("Overlay/FullHeart");
            Assets.EmptyHeart = Content.Load<Texture2D>("Overlay/EmptyHeart");
            Assets.TowerTexture = Content.Load<Texture2D>("TowerHex");
            Assets.GrassTexture = Content.Load<Texture2D>("Sprites/GrassHex");
            Assets.SandTexture = Content.Load<Texture2D>("Sprites/SandHex");
            Assets.MountainTexture = Content.Load<Texture2D>("Sprites/MountainHex");
            Assets.TowerTexture = Content.Load<Texture2D>("TowerHex");
            Assets.NexusTexture = Content.Load<Texture2D>("Sprites/NexusHex");
            Assets.NexusTextureOuter = Content.Load<Texture2D>("Sprites/NexusHexOuter");
            Assets.EnemyGoblinTexture = Content.Load<Texture2D>("Sprites/EnemyGoblin");
            Assets.FrostEnemyTexture = Content.Load<Texture2D>("Sprites/FrostEnemy");
            Assets.FrostEnemy2Texture = Content.Load<Texture2D>("Sprites/FrostEnemy2");
            Assets.FrostEnemy3Texture = Content.Load<Texture2D>("Sprites/FrostEnemy3");
            Assets.IntroTextTexture = Content.Load<Texture2D>("TextSprites/IntroText");
            Assets.IntroTextFont = Content.Load<SpriteFont>("Text/IntroText");
            Assets.PlayButton = Content.Load<Texture2D>("Buttons/play_btn");
            Assets.Frame = Content.Load<Texture2D>("Buttons/frame");
            Assets.PlayerName = Content.Load<SpriteFont>("Text/playername");
            Assets.Title = Content.Load<SpriteFont>("Text/title");
            Assets.NameBox = Content.Load<Texture2D>("Buttons/name");
            Assets.MapCreationFont = Content.Load<SpriteFont>("Text/MapCreation");
            Assets.TowerBuildingTexture = Content.Load<Texture2D>("Sprites/TowerBuildtile");
            Assets.BasetowerTexture = Content.Load<Texture2D>("Sprites/Basetower");
            Assets.FireEnemyTexture = Content.Load<Texture2D>("Sprites/FireElemental");
            Assets.FireEnemy2Texture = Content.Load<Texture2D>("Sprites/FireElemental2");
            Assets.FireEnemy3Texture = Content.Load<Texture2D>("Sprites/FireElemental3");
            Assets.FlyingEnemyTexture = Content.Load<Texture2D>("Sprites/FlyingEnemy");
            Assets.BasetowerTexture = Content.Load<Texture2D>("Sprites/Basetower");

            Assets.ArrowTexture = Content.Load<Texture2D>("Sprites/arrow");
            Assets.CannonBallTexture = Content.Load<Texture2D>("Sprites/cannon");
            Assets.MagicProjectileTexture = Content.Load<Texture2D>("Sprites/magic-purple");
            Assets.FiretowerTexture = Content.Load<Texture2D>("Sprites/Firetower");
            Assets.ArrowTexture = Content.Load<Texture2D>("Sprites/arrow");
            Assets.CannonBallTexture = Content.Load<Texture2D>("Sprites/cannon");
            Assets.MagicProjectileTexture = Content.Load<Texture2D>("Sprites/magic-purple");
            Assets.Archer = Content.Load<Texture2D>("Image/archerimage");
            Assets.Magic = Content.Load<Texture2D>("Image/magicimage");
            Assets.Artillery = Content.Load<Texture2D>("Image/artilleryimage");
            Assets.UpgradeButton = Content.Load<Texture2D>("Image/upgbutton");
            Assets.PriceButton = Content.Load<Texture2D>("Buttons/pricebutton");
            Assets.InputBox = Content.Load<Texture2D>("Image/inputbox");

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
