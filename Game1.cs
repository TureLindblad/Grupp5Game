using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Grupp5Game
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager _graphics {  get; private set; }
        public SpriteBatch _spriteBatch { get; private set; }
        public Grid MapGrid { get; private set; }
        public static Point MapDimensions = new Point(19, 8);
        public static Point WindowSize = new Point(1600, 900);
        public EnemySpawner Spawner { get; private set; }
        public List<Enemy> EnemyList {  get; private set; }
        public Scene CurrentScene { get; set; }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = WindowSize.X;
            _graphics.PreferredBackBufferHeight = WindowSize.Y;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            EnemyList = new List<Enemy>();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Assets.GrassTexture = Content.Load<Texture2D>("GrassHex");
            Assets.SandTexture = Content.Load<Texture2D>("SandHex");
            Assets.EnemyGoblinTexture = Content.Load<Texture2D>("EnemyGoblin");

            MapGrid = new Grid(MapDimensions);
            Spawner = new EnemySpawner();
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

            Spawner.Update(this);

            MapGrid.Update();

            for (int i = 0; i < EnemyList.Count; i++)
            {
                EnemyList[i].Update(this, gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGreen);

            _spriteBatch.Begin();

            MapGrid.Draw(this);

            foreach (Enemy enemy in EnemyList)
            {
                enemy.Draw(this);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
