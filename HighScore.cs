using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Grupp5Game
{
    public static class HighScore
    {
        public static List<int> LoadedHighScore { get; set; } = new List<int>();
        public static void LoadSavedData()
        {
            string fileName = "SavedData.json";

            string jsonData = File.ReadAllText(fileName);

            LoadedHighScore = JsonSerializer.Deserialize<List<int>>(jsonData);
        }

        public static void SaveData(int newData)
        {
            LoadSavedData();
            LoadedHighScore.Add(newData);
            string fileName = "SavedData.json";

            string data = JsonSerializer.Serialize(LoadedHighScore);
            
            File.WriteAllText(fileName, data);
        }

        public static void DrawHighScore()
        {
            foreach (int score in LoadedHighScore)
            {
                Globals.SpriteBatch.DrawString(
                    Assets.IntroTextFont,
                    $"{score}\n",
                    new Vector2(Globals.WindowSize.X / 2 - 280, Globals.WindowSize.Y - 90),
                    Color.Black);
            }
        }
    }
}
