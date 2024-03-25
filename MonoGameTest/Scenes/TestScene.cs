using MonoGameTest.GameObjects;
using MonoGameTest.Utils;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameTest.Scenes
{
    
    public class TestScene : Scene
    {
        private TileMap tileMap;
        private Camera camera;
        private GameObject mainCharacter;
        private MouseState lastMouseState = new();

        public TestScene() {
            name = "TestScene";
            camera = new();
            tileMap = new();
            mainCharacter = new Alex();
            gameObjects.Add(mainCharacter);
            gameObjects.Add(tileMap);
            
        }

        public override void update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            float scrollDiff = mouseState.ScrollWheelValue - lastMouseState.ScrollWheelValue;
            if (scrollDiff != 0)
            {
                camera.updateZoomBy(.1f * (scrollDiff / 120));
            }
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.update(gameTime);
            }
            CollisionMaster.checkStaticCollisions();
            camera.centerCamera(mainCharacter.position);


            lastMouseState = mouseState;
        }

        public override void draw(GameTime gameTime)
        {
            //https://community.monogame.net/t/transforming-objects-for-a-rendertarget2d/13959/2
            Globals.graphicsDevice.SetRenderTarget(Globals.RenderTarget);

            Globals.graphicsDevice.Clear(Color.CornflowerBlue);
            Globals.SpriteBatch.Begin(SpriteSortMode.BackToFront);
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.draw(gameTime);
            }


            Globals.SpriteBatch.End();

            Globals.graphicsDevice.SetRenderTarget(null);

            Globals.graphicsDevice.Clear(Color.Black);
            Globals.SpriteBatch.Begin(SpriteSortMode.BackToFront,
                BlendState.NonPremultiplied,
                SamplerState.PointWrap,
                DepthStencilState.Default,
                RasterizerState.CullNone,
                null,
                camera.getCameraMatrix());
            Globals.SpriteBatch.Draw(Globals.RenderTarget, new Vector2(0,0), Color.White);
            Globals.SpriteBatch.End();
        }
    }
}
