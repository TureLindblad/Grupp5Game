using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp5Game
{
    public class Assets
    {
        public static Texture2D GrassTexture;
        public static Texture2D SandTexture;
        public static Texture2D EnemyGoblinTexture;
        public static Texture2D FrostElementalTexture { get; internal set; }

        public static string GridMatrix =
            "1100000000011000000" +
            "0100000000100000000" +
            "0011000000110000000" +
            "0001000000010000000" +
            "0000110000001000000" +
            "0000010000001000000" +
            "0000001100011000000" +
            "0000000011100000000";

        public static string GridMatrix2 =
            "1000000000000000000" +
            "1000000000000000000" +
            "1000000000000000000" +
            "1000000000000000000" +
            "1000000000000000000" +
            "1000000000000000000" +
            "1000000000000000000" +
            "1000000000000000000";

    }
}
