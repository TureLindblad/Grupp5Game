using Microsoft.Xna.Framework;
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
        private Button playButton;

        public StartScreenScene()
        {
            playButton = new Button();
        }

        public override void Update()
        {
            playButton.Update(this);
        }

        public override void Draw()
        {
            if (playButton != null)
            {
                playButton.Draw(this);
            }

        }
    }

    public class MapScene : Scene
    {
        public Grid MapGrid { get; private set; }
        public static Point MapDimensions = new Point(19, 8);
        public EnemySpawner Spawner { get; private set; }
        public List<Enemy> EnemyList { get; private set; }

        public MapScene()
        {
            MapGrid = new Grid(MapDimensions);
            Spawner = new EnemySpawner();
            EnemyList = new List<Enemy>();
        }
        public override void Update()
        {
            Spawner.Update(this);

            MapGrid.Update();

            for (int i = 0; i < EnemyList.Count; i++)
            {
                EnemyList[i].Update(this);
            }
        }

        public override void Draw()
        {
            MapGrid.Draw(this);

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
