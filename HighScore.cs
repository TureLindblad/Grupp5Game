using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Grupp5Game
{
    public class HighScore
    {
        public static string CurrentPlayerName { get; set; }
        private readonly Texture2D Texture;
        public static List<Tuple<string, int>> LoadedHighScore { get; set; } = new List<Tuple<string, int>>();

        public HighScore()
        {
            Texture = Assets.LeaderBoard;
        }

        public static void LoadSavedData()
        {
            string fileName = "SavedData.json";

            string jsonData = File.ReadAllText(fileName);

            LoadedHighScore = JsonSerializer.Deserialize<List<Tuple<string, int>>>(jsonData);
        }

        public static void SaveData(Tuple<string, int> newData)
        {
            LoadSavedData();
            LoadedHighScore.Add(newData);
            string fileName = "SavedData.json";

            string data = JsonSerializer.Serialize(LoadedHighScore);
            
            File.WriteAllText(fileName, data);
        }

        private List<Tuple<string, int>> GetTopFive()
        {
            return LoadedHighScore
                .OrderByDescending(scoreTuple => scoreTuple.Item2)
                .Take(5)
                .ToList();
        }

        public void Draw()
        {
            Globals.SpriteBatch.Draw(Texture, 
                new Rectangle(50, Globals.WindowSize.Y / 2 - (int)(Texture.Width * 0.8) / 2, 
                    (int)(Texture.Width * 0.8), (int)(Texture.Height * 0.8)), 
                Color.White);

            string topFive = "";
            foreach (Tuple<string, int> score in GetTopFive())
            {
                topFive += $"{score.Item1}: {score.Item2} waves\n";
            }

            Globals.SpriteBatch.DrawString(
                Assets.IntroTextFont,
                topFive,
                new Vector2(Globals.WindowSize.X / 2 - 480, Globals.WindowSize.Y / 2),
                Color.Black);
        }
    }
}
