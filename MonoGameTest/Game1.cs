using System.Collections;

namespace MonoGameTest
{
    public class Game1 : Game
    {
        Sprite obama;
        ArrayList GameObjects = new ArrayList();
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private TileMap tileMap;
        private Camera camera;
        GameObject mainCharacter;
        MouseState lastMouseState = new();
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Globals.Content = Content;
            Globals.graphicsDevice = GraphicsDevice;
            tileMap = new();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            camera = new Camera();
            
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.SpriteBatch = _spriteBatch;
            mainCharacter = new Alex();
            
            GameObjects.Add(mainCharacter);
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A)){/* System.Diagnostics.Debug.WriteLine(gameTime); */}

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) { Exit(); }

            MouseState mouseState = Mouse.GetState();
            float scrollDiff = mouseState.ScrollWheelValue - lastMouseState.ScrollWheelValue;
            if(scrollDiff != 0)
            {
                camera.updateZoomBy(.001f * scrollDiff);
                Debug.WriteLine(camera.zoom);
            }

            camera.centerCamera(mainCharacter.sprite.position);
            foreach (GameObject  gameObject in GameObjects)
            {
                gameObject.update(gameTime);
            }
            lastMouseState = mouseState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            float zoom = 1f;
            Vector2 screenSize = Globals.getwindowScreenSize();
            GraphicsDevice.Clear(Color.CornflowerBlue);
            Globals.SpriteBatch.Begin(
                SpriteSortMode.BackToFront, 
                BlendState.NonPremultiplied, 
                SamplerState.PointClamp, 
                DepthStencilState.Default, 
                RasterizerState.CullNone, 
                null, 
               camera.getCameraMatrix()
                );
            tileMap.draw();
            foreach (GameObject gameObject in GameObjects)
            {
                gameObject.Draw(gameTime);
            }
            Globals.SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
