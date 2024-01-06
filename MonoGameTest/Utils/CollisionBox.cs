using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameTest.Utils
{
    public class CollisionBox
    {
        public int id = 0;
        public Rectangle bounds;
        public float maxDistanceForCollision = 0;
        public Vector2 origin;
        public bool isStatic = true;
        public GameObject parent;
        public Vector2 lastPosition = Vector2.Zero;
        public Vector2 currentPosition = Vector2.Zero;
        public Vector2[] points = { new(), new(), new(), new()};

        public CollisionBox(GameObject parent, Rectangle bounds) { 
            this.parent = parent;
            this.bounds = bounds;
            origin = new(bounds.Left + (bounds.Width / 2f), bounds.Top + (bounds.Height / 2f));
            maxDistanceForCollision = (float)Math.Sqrt((bounds.Width * bounds.Width) + (bounds.Height * bounds.Height));
            points[0] = new(bounds.Left, bounds.Top);
            points[1] = new(bounds.Left + bounds.Width, bounds.Top);
            points[2] = new(bounds.Left + bounds.Width, bounds.Top + bounds.Height);
            points[3] = new(bounds.Left, bounds.Top + bounds.Height);
        }

        private Boolean ccw(Vector2 a, Vector2 b, Vector2 c)
        {
            return (c.Y - a.Y) * (b.X - a.X) > (b.Y - a.Y) * (c.X - a.X);
        }
        private Boolean doesIntersect(Vector2[] l1, Vector2[] l2)
        {

            return ccw(l1[0], l2[0], l2[1]) != ccw(l1[1], l2[0], l2[1]) && ccw(l1[0], l1[1], l2[0]) != ccw(l1[0], l1[1], l2[1]);
        }

        public void resolveCollision(CollisionBox that)
        {
            if (!isStatic && parent != null)
            {
                Vector2[] originLine = { this.origin, that.origin };
                Boolean t = doesIntersect(originLine, this.getTopLine());
                Boolean r = doesIntersect(originLine, this.getRightLine());
                Boolean b = doesIntersect(originLine, this.getBottomLine());
                Boolean l = doesIntersect(originLine, this.getLeftLine());
                float allignTo = 0f;
                if (t) { allignTo = that.getBottomLine()[0].Y; }
                if (b) { allignTo = that.getTopLine()[0].Y; }
                if (l) { allignTo = that.getRightLine()[0].X; }
                if (r) { allignTo = that.getLeftLine()[0].X; }
                CollisionEvent collisionEvent = new(t, l, r, b, allignTo);
                Vector2 collisionLine = new(r || l ? 1 : 0, t || b ? 1 : 0);
                float slope = (originLine[1].Y - originLine[0].Y) / (originLine[1].X - originLine[0].X);
                Debug.WriteLine(that.id + "~~~~~~~~~~");
                Debug.WriteLine(collisionEvent.toString());
                parent.resolveEvent(Event.Collision, collisionEvent);
            }
        }

        public void updatePosition(Vector2 pos)
        {
            lastPosition = currentPosition;
            currentPosition = pos;
            bounds.Location = new((int)pos.X, (int)pos.Y);
            origin = new(bounds.Left + (bounds.Width / 2f), bounds.Top + (bounds.Height / 2f));
            points[0] = new(bounds.Left, bounds.Top);
            points[1] = new(bounds.Left + bounds.Width, bounds.Top);
            points[2] = new(bounds.Left + bounds.Width, bounds.Top + bounds.Height);
            points[3] = new(bounds.Left, bounds.Top + bounds.Height);

        }

        protected Vector2[] getTopLine()
        {
            Vector2[] line = { points[0], points[1] };
            return line;
        }

        protected Vector2[] getRightLine()
        {
            Vector2[] line = { points[1], points[2] };
            return line;
        }

        protected Vector2[] getBottomLine()
        {
            Vector2[] line = { points[2], points[3] };
            return line;
        }

        protected Vector2[] getLeftLine()
        {
            Vector2[] line = { points[3], points[0] };
            return line;
        }

    }
}
