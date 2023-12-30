using MonoGameTest.Utils;

namespace MonoGameTest.GameObjects
{
    internal class Alex : Sprite
    {
        int pointCycle = 0;
        float deltaSinceSpriteUpdate = 0;
        int direction = 0;
        CollisionBox collisionBox;
        Vector2 velocity = Vector2.Zero;

        public Alex()
        {
            collisionBox = new(this, new(new(0, 0), new(16, 32)));
            collisionBox.isStatic = false;
            CollisionMaster.registerCollisionBox(collisionBox);
            texture = Globals.getTextureAndHold("SpriteMaps/Alex");
            this.position = new(10 * 16, 10 * 16);
        }

        float speed = 75.0f;

        public override void update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            bool isMoving = false;
            velocity = Vector2.Zero;
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                direction = 2;
                velocity.Y = -1;
                isMoving = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                direction = 0;
                velocity.Y = 1;
                isMoving = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                direction = 3;
                velocity.X = -1;
                velocity.Normalize();
                isMoving = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                direction = 1;
                velocity.X = 1;
                velocity.Normalize();
                isMoving = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                direction = 1;
                velocity.X = 1;
                velocity.Normalize();
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
            setSprite(pointCycle, direction);
            velocity *= speed * delta;
            position += velocity;
            if (velocity != Vector2.Zero)
            {
                collisionBox.updatePosition(position);
            }

        }

        public override void draw(GameTime gameTime)
        {
            //Globals.SpriteBatch.Draw(Globals.getWhite(), collisionBox.bounds, Color.Aqua);
            base.draw(gameTime);
        }

        public override void resolveEvent(Event evt) { 
            if(evt == Event.Collision)
            {
                position -= velocity;
                collisionBox.updatePosition(position);
            }
        }
    }
}
