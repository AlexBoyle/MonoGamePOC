using System.Collections.Generic;
namespace MonoGameTest.Utils
{

    public class GameObject
    {
        public Vector2 position { get; set; }
        public GameObject() { }

        public virtual void update(GameTime gameTime) { }

        public virtual void draw(GameTime gameTime) { }

        public virtual void resolveEvent(Event evt, object details) { }
        public virtual void resolveEvents(Event evt, object details) { }
        public virtual void onDestroy() { }

    }
}
