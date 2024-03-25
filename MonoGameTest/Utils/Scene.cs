using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameTest.Utils
{
    public class Scene
    {
        protected string name = "Base Scene";

        protected ArrayList gameObjects = new();

        public Scene() { Debug.WriteLine("Scene \"" + this.name + "\" has initalized"); }
        public virtual void update(GameTime gameTime) { }

        public virtual void draw(GameTime gameTime) { }

        public virtual void onDestroy() { }

    }
}
