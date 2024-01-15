using System;
using System.Collections.Generic;

namespace MonoGameTest.Physics
{
    public static class CollisionMaster
    {
        static List<CollisionBox> staticCollisionBoxes = new();
        static List<CollisionBox> dynamicCollisionBoxes = new();

        private static Rectangle getOverlap(Rectangle r1, Rectangle r2)
        {
            return Rectangle.Intersect(r1, r2);
        }

        public static int registerCollisionBox(CollisionBox collisionBox)
        {
            if (collisionBox.isStatic)
            {
                staticCollisionBoxes.Add(collisionBox);
            }
            else
            {
                dynamicCollisionBoxes.Add(collisionBox);
            }
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
                List<CollisionEvent> collisionsToResolve = new();
                foreach (CollisionBox staticCollisionBox in staticCollisionBoxes)
                {
                    if (Vector2.Distance(dynamicCollisionBox.origin, staticCollisionBox.origin) < dynamicCollisionBox.maxDistanceForCollision + staticCollisionBox.maxDistanceForCollision)
                    {
                        Rectangle overlap = getOverlap(dynamicCollisionBox.bounds, staticCollisionBox.bounds);
                        if (!overlap.IsEmpty)
                        {
                            collisionsToResolve.Add(new(dynamicCollisionBox, staticCollisionBox));

                        }
                    }
                }
                if (collisionsToResolve.Count > 0)
                {
                    dynamicCollisionBox.resolveCollisions(collisionsToResolve);
                }
            }
        }

    }
}
