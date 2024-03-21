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
            if (CanSpawn /*&& EnemiesSpawned < NumberOfEnemiesToSpawn*/)
            {
                for (int i = 0; i < NumberOfEnemiesToSpawn; i++)
                {
                    Enemy newEnemy = new GoblinEnemy(Assets.EnemyGoblinTexture);
                    mapScene.EnemyList.Add(newEnemy);
                    /*EnemiesSpawned++;

                    foreach (Enemy existingEnemy in mapScene.EnemyList)
                    {
                        if (newEnemy !=  existingEnemy && Collision.CheckCollision(newEnemy, mapScene.EnemyList))
                        {   
                            Vector2 displacement = Collision.CalculateDisplacement(newEnemy.Bounds, existingEnemy.Bounds);
                            newEnemy.Position += displacement;
                        } 
                    }

                    if (EnemiesSpawned >= NumberOfEnemiesToSpawn) { break; }*/
                }
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
