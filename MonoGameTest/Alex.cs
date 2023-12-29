using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using System.Security.Policy;
using System.Diagnostics;

namespace MonoGameTest
{
    internal class Alex : GameObject
    {
        int pointCycle = 0;
        float deltaSinceSpriteUpdate = 0;
        int direction = 0;
        public Alex() {
            sprite = new(Globals.getTextureAndHold("Alex"), Vector2.Zero);
        }

        float speed = 75.0f;

        public override void update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            bool isMoving = false;
            Vector2 change = Vector2.Zero;
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                direction = 2;
                change.Y = -1;
                isMoving = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                direction = 0;
                change.Y = 1;
                isMoving = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                direction = 3;
                change.X =  -1;
                change.Normalize();
                isMoving = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                direction = 1;
                change.X = 1;
                change.Normalize();
                isMoving = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                direction = 1;
                change.X = 1;
                change.Normalize();
                isMoving = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                direction = 4;
                isMoving = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                direction = 7;
                isMoving = true;
            }
            if (isMoving)
            {
                deltaSinceSpriteUpdate += delta;
                if (deltaSinceSpriteUpdate > .13f)
                {
                    pointCycle = (pointCycle + 1) % 4;
                    deltaSinceSpriteUpdate = 0;
                    deltaSinceSpriteUpdate = 0;
                }
            }
            else
            {
                deltaSinceSpriteUpdate = 0;
                pointCycle = 0;
            }
            sprite.setSprite(pointCycle, direction);
            //System.Diagnostics.Debug.WriteLine(change.X);
            change *= speed * delta;
            sprite.position += change;

        }
    }
}
