namespace MonoGameTest
{
    
    internal abstract class GameObject
    {
        public Sprite sprite = null;
        protected GameObject[] children = null;

        public GameObject() { }
        public GameObject(Sprite sprite)
        {
            this.sprite = sprite;
        }

        public abstract void update(GameTime gameTime);

        public void Draw(GameTime gameTime)
        {
            sprite.Draw();
        }

    }
}
