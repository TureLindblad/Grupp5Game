﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp5Game
{
    public class EnemySpawner
    {
        private bool CanSpawn = true;
        public int NumberOfEnemiesToSpawn;
        public EnemySpawner()
        {
            NumberOfEnemiesToSpawn = 5;
            CanSpawn = true;
        }
        public void Update(Game1 game)
        {
            if (CanSpawn)
            {
                for(int i = 0; i < NumberOfEnemiesToSpawn; i++)
                {
                    game.EnemyList.Add(new GoblinEnemy(Assets.EnemyGoblinTexture));
                    EnemeySpawnTimer();
                }
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
