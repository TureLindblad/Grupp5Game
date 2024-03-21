using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp5Game
{
    public static class Collision
    {

        public static Tuple<bool, bool> CheckCollision(Enemy enemy1, List<Enemy> enemyList)
        {
            foreach (Enemy enemy2 in enemyList)
            {
                if (enemy1.Bounds.Intersects(enemy2.Bounds) && enemy1 != enemy2)
                {
                    /*                    Vector2 displacement = CalculateDisplacement(enemy1.BoundsWithNextMovement, enemy2.Bounds);

                                        bool collisionX = Math.Abs(displacement.X) > Math.Abs(displacement.Y);

                                        bool collisionY = Math.Abs(displacement.X) < Math.Abs(displacement.Y);*/

                    bool collisionDirectionIsX =
                        Math.Abs(enemy1.Bounds.X - enemy2.Bounds.X) >
                        Math.Abs(enemy1.Bounds.Y - enemy2.Bounds.Y);

                    if (collisionDirectionIsX) return Tuple.Create(true, false);
                    else return Tuple.Create(false, true);

                    //return Tuple.Create(collisionX, collisionY);

                }
            }

            return Tuple.Create(false, false);
        }


    }
}


