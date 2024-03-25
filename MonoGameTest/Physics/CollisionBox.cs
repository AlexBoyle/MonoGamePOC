using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameTest.Physics
{
    public class CollisionBox
    {
        public int id = 0;
        public Rectangle bounds;
        public float maxDistanceForCollision = 0;
        public Vector2 origin;
        public bool isStatic = true;
        public GameObject parent;
        public Vector2[] points = { new(), new(), new(), new() };

        public CollisionBox(GameObject parent, Rectangle bounds)
        {
            this.parent = parent;
            this.bounds = bounds;
            updatePosition(bounds.Location);
        }

        private bool ccw(Vector2 a, Vector2 b, Vector2 c)
        {
            return (c.Y - a.Y) * (b.X - a.X) > (b.Y - a.Y) * (c.X - a.X);
        }
        private bool doesIntersect(Vector2[] l1, Vector2[] l2)
        {

            return ccw(l1[0], l2[0], l2[1]) != ccw(l1[1], l2[0], l2[1]) && ccw(l1[0], l1[1], l2[0]) != ccw(l1[0], l1[1], l2[1]);
        }

        public void resolveCollisions(List<CollisionEvent> collisionsToResolve)
        {
            parent.resolveEvents(Event.Collision, collisionsToResolve);
        }

        public void updatePosition(Vector2 pos)
        {
            updatePosition(new Point((int)pos.X, (int)pos.Y));
        }
        public void updatePosition(Point pos)
        {
            bounds.Location = new(pos.X, pos.Y);
            maxDistanceForCollision = (float)Math.Sqrt(bounds.Width * bounds.Width + bounds.Height * bounds.Height);
            origin = new(bounds.Left + bounds.Width / 2f, bounds.Top + bounds.Height / 2f);
            points[0] = new(bounds.Left, bounds.Top);
            points[1] = new(bounds.Left + bounds.Width, bounds.Top);
            points[2] = new(bounds.Left + bounds.Width, bounds.Top + bounds.Height);
            points[3] = new(bounds.Left, bounds.Top + bounds.Height);
        }

        public float getTopAlignemnt()
        {
            return points[0].Y;
        }
        public float getBottomAlignemnt()
        {
            return points[2].Y;
        }
        public float getLeftAlignemnt()
        {
            return points[3].X;
        }
        public float getRightAlignemnt()
        {
            return points[1].X;
        }

        public Vector2[] getTopLine()
        {
            Vector2[] line = { points[0], points[1] };
            return line;
        }

        public Vector2[] getRightLine()
        {
            Vector2[] line = { points[1], points[2] };
            return line;
        }

        public Vector2[] getBottomLine()
        {
            Vector2[] line = { points[2], points[3] };
            return line;
        }

        public Vector2[] getLeftLine()
        {
            Vector2[] line = { points[3], points[0] };
            return line;
        }
    }
}
