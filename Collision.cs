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
        public static bool CheckCollision(Enemy enemy1 , List<Enemy> enemyList)
        {
            foreach (Enemy enemy2 in enemyList)
            {
                if (enemy1.Bounds.Intersects(enemy2.Bounds) && enemy1 != enemy2 )
                {
                    HandleCollision(enemy1, enemy2);
                    return true;
                }
            }
            return false;
        }

        public static bool HandleCollision(Enemy enemy1, Enemy enemy2)
        {
            Rectangle intersection = Rectangle.Intersect(enemy1.Bounds, enemy2.Bounds);

            // If there is an intersection
            if (!intersection.IsEmpty)
            {
                // Calculate the minimum displacement needed to separate the rectangles
                Vector2 displacement = Vector2.Zero;

                // Calculate displacement along X-axis
                if (intersection.Width < intersection.Height)
                {
                    if (enemy1.Bounds.Center.X < enemy2.Bounds.Center.X)
                        displacement.X = intersection.Width;
                    else
                        displacement.X = -intersection.Width;
                }
                // Calculate displacement along Y-axis
                else
                {
                    if (enemy1.Bounds.Center.Y < enemy2.Bounds.Center.Y)
                        displacement.Y = intersection.Height;
                    else
                        displacement.Y = -intersection.Height;
                }

                // Move enemy1 away from enemy2
                enemy1.Position += displacement;

                // Indicate that a collision occurred and was handled
                return true;
            }

            // No collision occurred
            return false;
        }
    }
}
