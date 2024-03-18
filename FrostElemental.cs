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
        public FrostElemental(Texture2D texture) : base(texture)
        {

            Position = new Vector2(0, 0);
            Size = 2000;
            Velocity = new Vector2(0, 0);
            Speed = 5;

        }
    }
}

