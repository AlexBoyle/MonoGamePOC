using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameTest.Utils
{
    public class CollisionBox
    {
        public Rectangle bounds;
        public float maxDistanceForCollision = 0;
        public Vector2 origin;
        public bool isStatic = true;
        public GameObject parent;
        public Vector2 lastPosition = Vector2.Zero;
        public Vector2 currentPosition = Vector2.Zero;

        public CollisionBox(GameObject parent, Rectangle bounds) { 
            this.parent = parent;
            this.bounds = bounds;
            origin = new(bounds.Location.X - bounds.Width / 2, bounds.Location.Y - bounds.Height / 2);
            maxDistanceForCollision = (float)Math.Sqrt((bounds.Width * bounds.Width) + (bounds.Height * bounds.Height));
        }

        public void resolveCollision()
        {
            if (!isStatic && parent != null)
            {
                parent.resolveEvent(Event.Collision);
            }
        }

        public void updatePosition(Vector2 pos)
        {
            lastPosition = currentPosition;
            currentPosition = pos;
            bounds.Location = new((int)pos.X, (int)pos.Y);
            origin = new(bounds.Location.X - bounds.Width / 2, bounds.Location.Y - bounds.Height / 2);

        }

    }
}
