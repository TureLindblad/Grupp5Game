using Microsoft.Xna.Framework;
using System.Threading.Tasks;

namespace Grupp5Game 
{
    public class EnemySpawner
    {
        private bool CanSpawn = true;
        public int NumberOfEnemiesToSpawn;
        private int EnemiesSpawned;
        public EnemySpawner()
        {
            NumberOfEnemiesToSpawn = 1;
            CanSpawn = true;
        }
        public void Update(MapScene mapScene)
        {
            if (CanSpawn)
            {
                mapScene.EnemyList.Add(new GoblinEnemy(Assets.EnemyGoblinTexture));
                EnemeySpawnTimer();
            }

            
        }

        private async void EnemeySpawnTimer()
        {
            CanSpawn = false;
            await Task.Delay(1000);
            CanSpawn = true;
        }
    }
}
