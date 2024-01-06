using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameTest.Utils
{
    public enum Event
    {
        Collision
    }

    public class CollisionEvent
    {
        public Boolean top = false;
        public Boolean left = false;
        public Boolean right = false;
        public Boolean bottom = false;
        public float ajust = 0;

        public CollisionEvent(bool top, bool left, bool right, bool bottom, float ajust)
        {
            this.top = top;
            this.left = left;
            this.right = right;
            this.bottom = bottom;
            this.ajust = ajust;
        }
        public string toString()
        {
            return "{ top: " + top + ", bottom: " + bottom + ", Left: " + left + ", Right: " + right + "}";
        }
    }
    
}
