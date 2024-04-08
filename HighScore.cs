using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

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

            if (File.Exists(fileName))
            {
                string jsonData = File.ReadAllText(fileName);

                LoadedHighScore = JsonSerializer.Deserialize<List<Tuple<string, int>>>(jsonData);
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    LoadedHighScore.Add(Tuple.Create("", 0));
                    
                }
            }
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
            var query = LoadedHighScore
                .OrderByDescending(scoreTuple => scoreTuple.Item2)
                .Take(5)
                .ToList();

            for (int i = 0; i < 5; i++)
            {
                if (i == query.Count - 1 || query.Count == 0)
                {
                    query.Add(Tuple.Create("", 0));
                }
            }

            return query;
        }

        public void Draw()
        {
            Globals.SpriteBatch.Draw(Texture, 
                new Rectangle(50, Globals.WindowSize.Y / 2 - (int)(Texture.Width * 0.8) / 2, 
                    (int)(Texture.Width * 0.8), (int)(Texture.Height * 0.8)), 
                Color.White);

            var topFive = GetTopFive();

            Globals.SpriteBatch.DrawString(
            Assets.IntroTextFont,
                $"[1] {topFive[0].Item1}: {topFive[0].Item2} score",
                new Vector2(100, Globals.WindowSize.Y / 2 - (int)(Texture.Width * 0.8) / 2 + 75),
                Color.Black);

            Globals.SpriteBatch.DrawString(
                Assets.IntroTextFont,
                $"[2] {topFive[1].Item1}: {topFive[1].Item2} score",
                new Vector2(100, Globals.WindowSize.Y / 2 - (int)(Texture.Width * 0.8) / 2 + 150),
                Color.Black);

            Globals.SpriteBatch.DrawString(
                Assets.IntroTextFont,
                $"[3] {topFive[2].Item1}: {topFive[2].Item2} score",
                new Vector2(100, Globals.WindowSize.Y / 2 - (int)(Texture.Width * 0.8) / 2 + 225),
                Color.Black);

            Globals.SpriteBatch.DrawString(
                Assets.IntroTextFont,
                $"[4] {topFive[3].Item1}: {topFive[3].Item2} score",
                new Vector2(100, Globals.WindowSize.Y / 2 - (int)(Texture.Width * 0.8) / 2 + 300),
                Color.Black);

            Globals.SpriteBatch.DrawString(
                Assets.IntroTextFont,
                $"[5] {topFive[4].Item1}: {topFive[4].Item2} score",
                new Vector2(100, Globals.WindowSize.Y / 2 - (int)(Texture.Width * 0.8) / 2 + 375),
                Color.Black);
        }
    }
}
