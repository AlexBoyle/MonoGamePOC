using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameTest.Utils
{
    public static class DebugResorce
    {

        static DebugResorce()
        {
            whiteRectangle = new Texture2D(Globals.graphicsDevice, 1, 1);
            whiteRectangle.SetData(new[] { Color.White });
        }
        public static Texture2D whiteRectangle { get; set; }
       
    }
}
