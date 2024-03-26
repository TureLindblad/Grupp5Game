using Microsoft.Xna.Framework;
using System.Threading.Tasks;

namespace Grupp5Game 
{
    public class EnemySpawner
    {
        private bool CanSpawn = true;
        public int NumberOfEnemiesToSpawn;
        private int SpawnTowerAttacker;
        public EnemySpawner()
        {
            NumberOfEnemiesToSpawn = 1;
            CanSpawn = true;
            SpawnTowerAttacker = 0;
        }
        public void Update(PlayMapScene mapScene)
        {
            if (CanSpawn)
            {
                if (SpawnTowerAttacker < 4)
                {
                    mapScene.EnemyList.Add(new FrostEnemy(Assets.FrostEnemyTexture));
                    SpawnTowerAttacker++;
                }
                else mapScene.EnemyList.Add(new GoblinEnemy(Assets.EnemyGoblinTexture));
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
