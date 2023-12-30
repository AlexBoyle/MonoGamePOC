


using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MonoGameTest
{
    public class Game1 : Game
    {
        Sprite obama;
        ArrayList gameObjects = new ArrayList();
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private TileMap tileMap;
        private Camera camera;
        GameObject mainCharacter;
        MouseState lastMouseState = new();
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Globals.graphicsDeviceManager = _graphics;
            Globals.gameWindow = Window;
            

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Globals.Content = Content;
            Globals.graphicsDevice = GraphicsDevice;

            Globals.switchToFullScreen(true, false);
            tileMap = new();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            camera = new Camera();
            
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.SpriteBatch = _spriteBatch;
            mainCharacter = new Alex();

            gameObjects.Add(mainCharacter);
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A)){/* System.Diagnostics.Debug.WriteLine(gameTime); */}

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) { Exit(); }

            MouseState mouseState = Mouse.GetState();
            float scrollDiff = mouseState.ScrollWheelValue - lastMouseState.ScrollWheelValue;
            if(scrollDiff != 0)
            {
                camera.updateZoomBy(.1f * (scrollDiff/120));
            }

            

            foreach (GameObject  gameObject in gameObjects)
            {
                gameObject.update(gameTime);
            }
            CollisionMaster.checkStaticCollisions();
            camera.centerCamera(mainCharacter.position);


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
            tileMap.draw(gameTime);
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.draw(gameTime);
            }
            Globals.SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
