using System.Collections.Generic;

namespace MonoGameTest.Core
{
    public class Game : Microsoft.Xna.Framework.Game
    {   
        private Scene currentScene = null;

        public Game()
        {
            Globals.graphicsDeviceManager = new GraphicsDeviceManager(this);
            Globals.gameWindow = Window;
            List<Scene> scenes = new();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Globals.Content = Content;
            Globals.graphicsDevice = GraphicsDevice;
            Globals.SpriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.RenderTarget = new RenderTarget2D(Globals.graphicsDevice, (int)Globals.getwindowScreenSize().X, (int)Globals.getwindowScreenSize().Y, false, Globals.graphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.Depth24);


            currentScene = new Scenes.TestScene();
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) { Exit(); }
            if (Keyboard.GetState().IsKeyDown(Keys.F)) { Globals.switchToFullScreen(true, false); }

            if (currentScene != null)
            {
                currentScene.update(gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if(currentScene != null)
            {
                currentScene.draw(gameTime);
            }

            base.Draw(gameTime);
        }
    }
}
