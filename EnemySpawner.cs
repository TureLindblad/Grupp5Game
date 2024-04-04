using System;
using System.Threading.Tasks;

namespace Grupp5Game 
{
    public struct CurrentWave
    {
        public static int Tier1Enemy = 3;
        public static int Tier2Enemy = 0;
        public static int Tier3Enemy = 0;
        public static int EnemyWave = 0;
        public static int FlyingEnemies = 2;
        public static int FastEnemies = 1;
        public static int BossEnemies = 0;
    }
    public class EnemySpawner
    {
        private bool CanSpawn = true;
        private int Tier1Enemy;
        private int Tier2Enemy;
        private int Tier3Enemy;
        private int EnemyWave;
        private int FlyingEnemies;
        private int FastEnemies;
        private int BossEnemies;
        public EnemySpawner()
        {
            Tier1Enemy = 0;
            Tier2Enemy = 0; 
            Tier3Enemy = 0;
            FlyingEnemies = 0;
            EnemyWave = 0;
            FastEnemies = 0;
            BossEnemies = 0;
            CanSpawn = true;
        }
        public void Update(PlayMapScene mapScene)
        {
            if (CanSpawn)
            {
                if (Tier1Enemy < CurrentWave.Tier1Enemy)
                {
                    mapScene.EnemyList.Add(GetTier1Enemy());
                    Tier1Enemy+=2;
                    EnemeySpawnTimer();
                }
                else if (Tier2Enemy < CurrentWave.Tier2Enemy)
                {
                    mapScene.EnemyList.Add(GetTier2Enemy());
                    Tier2Enemy++;
                    EnemeySpawnTimer();
                }
                else if (Tier3Enemy < CurrentWave.Tier3Enemy)
                {
                    mapScene.EnemyList.Add(GetTier3Enemy());
                    Tier3Enemy++;
                    EnemeySpawnTimer();
                }
                else if (FlyingEnemies < CurrentWave.FlyingEnemies)
                {
                    mapScene.EnemyList.Add(new FlyingEnemy(Assets.FlyingEnemyTexture));
                    FlyingEnemies++;
                    EnemeySpawnTimer();
                }
                else if (FastEnemies < CurrentWave.FastEnemies)
                {
                    mapScene.EnemyList.Add(new FastEnemy(Assets.FastEnemyTexture));
                    FastEnemies++;
                    EnemeySpawnTimer();
                }
                else if (BossEnemies  < CurrentWave.BossEnemies)
                {
                    mapScene.EnemyList.Add(new BossEnemy(Assets.BossEnemyTexture));
                    BossEnemies++;
                    EnemeySpawnTimer();
                }

                HandleEndOfWave(mapScene);
            }
        }
        
        private Tier1Enemy GetTier1Enemy()
        {
            Random rnd = new Random();
            Tier1Enemy newEnemey = null;

            switch(rnd.Next(0,3))
            {
                case 0:
                    newEnemey = new GoblinEnemy(Assets.EnemyGoblinTexture);
                    break;
                case 1:
                    newEnemey = new FrostEnemy(Assets.FrostEnemyTexture);
                    break;
                case 2:
                    newEnemey = new FireEnemy(Assets.FireEnemy2Texture);
                    break;
            }

            return newEnemey;
        }

        private Tier2Enemy GetTier2Enemy()
        {
            Random rnd = new Random();
            Tier2Enemy newEnemey = null;

            switch (rnd.Next(0, 2))
            {
                case 0:
                    newEnemey = new FireEnemy2(Assets.FireEnemyTexture);
                    break;
                case 1:
                    newEnemey = new FrostEnemy2(Assets.FrostEnemy2Texture);
                    break;

            }

            return newEnemey;
        }

        private Tier3Enemy GetTier3Enemy()
        {
            Random rnd = new Random();
            Tier3Enemy newEnemey = null;

            switch (rnd.Next(0, 2))
            {
                case 0:
                    newEnemey = new FireEnemy3(Assets.FireEnemy3Texture);
                    break;
                case 1:
                    newEnemey = new FrostEnemy3(Assets.FrostEnemy3Texture);
                    break;              
            }
            return newEnemey;
        }

        private void HandleEndOfWave(PlayMapScene mapScene)
        {
            if (mapScene.EnemyList.Count == 0)
            {
/*                WaveTimer();*/
                mapScene.GameOverlay.CurrentWave++;

                Tier1Enemy = 0;
                Tier2Enemy = 0;
                Tier3Enemy = 0;
                FlyingEnemies = 0;
                FastEnemies = 0;
                
                EnemyWave++;

                if (EnemyWave % 4 == 0)
                {
                    CurrentWave.Tier1Enemy--;
                    CurrentWave.FastEnemies++;
                }
                if (EnemyWave % 2== 0)
                {
                    CurrentWave.Tier2Enemy++;
                }
                if (EnemyWave % 3 == 0)
                {
                    CurrentWave.Tier3Enemy++;
                }
                if (EnemyWave % 2 == 0)
                {
                    CurrentWave.FlyingEnemies++;
                }
                if (EnemyWave % 5 == 0)
                {
                    CurrentWave.BossEnemies++;
                }
            }
        }
        private async void EnemeySpawnTimer()
        {
            CanSpawn = false;
            await Task.Delay(1000);
            CanSpawn = true;
        }
        private async void WaveTimer()
        {
            CanSpawn = false;
            await Task.Delay(15000);
            CanSpawn = true;
        }
    }
}
