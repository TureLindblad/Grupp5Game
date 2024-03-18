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
        public override void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space)) Game1.CurrentScene = new MapScene();
        }

        public override void Draw()
        {

        }
    }

    public class StartScreenScene : Scene
    {
        public override void Update()
        {

        }

        public override void Draw()
        {

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
