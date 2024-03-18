using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp5Game
{
    public class FrostElemental : Enemy
    {
        private Texture2D Texture;
        public int Size { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        private int Speed;
        private List<Tile> CompletedTileList;
        public FrostElemental(Texture2D texture) : base(texture)
        {

            Position = new Vector2(0, 0);
            Size = 50;
            Velocity = new Vector2(0, 0);
            Speed = 1;
            CompletedTileList = new List<Tile>();
        }
    }
}

