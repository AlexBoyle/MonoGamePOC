using System;
using System.Collections.Generic;

namespace MonoGameTest.Utils
{
    public static class CollisionMaster
    {
        static List<CollisionBox> staticCollisionBoxes = new();
        static List<CollisionBox> dynamicCollisionBoxes = new(); 

        private static Rectangle getOverlap(Rectangle r1, Rectangle r2)
        {
            return Rectangle.Intersect(r1, r2);
        }

        private static Boolean areRectanglesOverlapping(Rectangle r1, Rectangle r2)
        {
            
            return 
                !(r1.Left > r2.Left + r2.Width) &&
                !(r1.Left + r1.Width < r2.Left) &&
                !(r1.Top > r2.Top + r2.Height) &&
                !(r1.Top + r1.Height < r2.Top);
        }

        public static int registerCollisionBox(CollisionBox collisionBox)
        {
            if(collisionBox.isStatic)
            {
                staticCollisionBoxes.Add(collisionBox);
            }
            else
            {
                dynamicCollisionBoxes.Add(collisionBox);
            }
            Debug.WriteLine("staticCollisionBoxes  Length: " + staticCollisionBoxes.Count);
            Debug.WriteLine("dynamicCollisionBoxes Length: " + dynamicCollisionBoxes.Count);
            return 0;
        }

        public static int unRegisterCollisionBox(int id)
        {
            return 0;
        }

        public static void checkStaticCollisions()
        {
            foreach (CollisionBox dynamicCollisionBox in dynamicCollisionBoxes)
            {
                foreach (CollisionBox staticCollisionBox in staticCollisionBoxes)
                {
                    if (Vector2.Distance(dynamicCollisionBox.origin, staticCollisionBox.origin) < dynamicCollisionBox.maxDistanceForCollision + staticCollisionBox.maxDistanceForCollision)
                    {
                        Rectangle overlap = getOverlap(dynamicCollisionBox.bounds, staticCollisionBox.bounds);
                        if (!(overlap.IsEmpty))
                        {
                            dynamicCollisionBox.resolveCollision(staticCollisionBox);
                        }
                    }
                }
            }
        }

    }
}
