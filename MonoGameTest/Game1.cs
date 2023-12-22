using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Linq;
using System.Diagnostics;

namespace MonoGameTest
{
    public class Game1 : Game
    {
        int w; 
        int h;
        Sprite obama;
        ArrayList GameObjects = new ArrayList();
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private TileMap tileMap;
        private Camera camera;
        GameObject mainCharacter;
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
            
            tileMap = new();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            w = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            h = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            camera = new Camera();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.SpriteBatch = _spriteBatch;
            mainCharacter = new Alex();
            GameObjects.Add(mainCharacter);
            
        }

        protected override void Update(GameTime gameTime)
        {
            
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
               // System.Diagnostics.Debug.WriteLine(gameTime);
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            foreach(GameObject  gameObject in GameObjects)
            {
                gameObject.update(gameTime);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            float zoom = 3f;
            int width = GraphicsDevice.PresentationParameters.Bounds.Width;
            int height = GraphicsDevice.PresentationParameters.Bounds.Height;
            GraphicsDevice.Clear(Color.CornflowerBlue);
            Globals.SpriteBatch.Begin(
                SpriteSortMode.BackToFront, 
                BlendState.NonPremultiplied, 
                SamplerState.PointClamp, 
                DepthStencilState.Default, 
                RasterizerState.CullNone, 
                null, 
                Matrix.CreateScale(new Vector3(zoom, zoom, 1)) * 
                Matrix.CreateTranslation(new Vector3(-mainCharacter.sprite.position.X* zoom, -mainCharacter.sprite.position.Y * zoom, 0)) *
                //Matrix.CreateTranslation(new Vector3(0, 0, 0)) *
                Matrix.CreateTranslation(new Vector3(width*.5f,height*.5f, 0)) 
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
