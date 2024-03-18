using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp5Game 
{
    public class EnemySpawner
    {
        private bool CanSpawn;

        public EnemySpawner()
        {
            CanSpawn = true;
        }
        public void Update(Game1 game)
        {
            if (CanSpawn)
            {
                game.EnemyList.Add(new Enemy());
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
