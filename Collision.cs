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
                if (enemy1.Bounds.Intersects(enemy2.Bounds) && enemy1 != enemy2)
                {
                    Vector2 displacement = CalculateDisplacement(enemy1.Bounds, enemy2.Bounds);
                    enemy1.Position += displacement;

                    return true;
                }
            }

            return false;

/*            foreach (Enemy enemy2 in enemyList)
            {
                if (enemy1.Bounds.Intersects(enemy2.Bounds) && enemy1 != enemy2 )
                {
                    HandleCollision(enemy1, enemy2);
                    return true;
                }
            }
            return false;*/
        }

        public static Vector2 CalculateDisplacement (Rectangle bounds1, Rectangle bounds2)
        {
            Rectangle intersection = Rectangle.Intersect(bounds1, bounds2);
            Vector2 displacement = Vector2.Zero;

            if (!intersection.IsEmpty)
            {
                displacement.X = bounds1.Center.X < bounds2.Center.X ? intersection.Width: - intersection.Width;
            }

            else
            {
                displacement.Y = bounds1.Center.Y < bounds2.Center.Y ? intersection.Height: - intersection.Height;
            }


            return displacement;
        }

        public static bool HandleCollision(Enemy enemy1, Enemy enemy2)
        {
            Rectangle intersection = Rectangle.Intersect(enemy1.Bounds, enemy2.Bounds);

            if (!intersection.IsEmpty)
            {

                Vector2 displacement = Vector2.Zero;

                if (intersection.Width < intersection.Height)
                {
                    if (enemy1.Bounds.Center.X < enemy2.Bounds.Center.X)
                        displacement.X = intersection.Width;
                    else
                        displacement.X = -intersection.Width;
                }

                else
                {
                    if (enemy1.Bounds.Center.Y < enemy2.Bounds.Center.Y)
                        displacement.Y = intersection.Height;
                    else
                        displacement.Y = -intersection.Height;
                }

                enemy1.Position += displacement;

                return true;
            }

            return false;
        }
    }
}
