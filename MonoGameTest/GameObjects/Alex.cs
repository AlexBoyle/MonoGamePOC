using MonoGameTest.Utils;
using System.Globalization;

namespace MonoGameTest.GameObjects
{
    internal class Alex : Sprite
    {
        int pointCycle = 0;
        float deltaSinceSpriteUpdate = 0;
        int direction = 0;
        CollisionBox collisionBox;
        Vector2 velocity = Vector2.Zero;
        NumberFormatInfo numberFormat = new NumberFormatInfo
        {
            NumberDecimalSeparator = ",",
            NumberGroupSeparator = "."
        };
        string lastCol = "";

        public Alex()
        {
            collisionBox = new(this, new(new(0, 0), new(16, 16)));
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

        string formatFloat(float f)
        {
            return f.ToString("###,0.00", numberFormat);
        }

        public override void draw(GameTime gameTime)
        {
            Globals.SpriteBatch.DrawString( Globals.getSpriteFont("fonts/default"), "Vel: " + formatFloat(velocity.X) + " : " + formatFloat(velocity.Y), new Vector2(collisionBox.bounds.X, collisionBox.bounds.Y - 90), Color.Black);
            Globals.SpriteBatch.DrawString( Globals.getSpriteFont("fonts/default"), "LastCol: " + lastCol, new Vector2(collisionBox.bounds.X, collisionBox.bounds.Y - 75), Color.Black);
            Globals.SpriteBatch.DrawString( Globals.getSpriteFont("fonts/default"), "x: "  + formatFloat(position.X) + " :" + formatFloat(position.Y), new Vector2(collisionBox.bounds.X, collisionBox.bounds.Y - 60), Color.Black);
            Globals.SpriteBatch.DrawString( Globals.getSpriteFont("fonts/default"), "x: "  + collisionBox.bounds.X + "      :" + collisionBox.bounds.Y, new Vector2(collisionBox.bounds.X, collisionBox.bounds.Y - 45), Color.Black);
            Globals.SpriteBatch.Draw(Globals.getWhite(), collisionBox.bounds, Color.Aqua);
            base.draw(gameTime);
        }

        public override void resolveEvent(Event evt, object d) { 
            if(evt == Event.Collision)
            {
                CollisionEvent collisionEvent = (CollisionEvent)d;
                if (collisionEvent.top) { position = new(position.X, collisionEvent.ajust ); lastCol = "Top"; }
                if (collisionEvent.bottom) { position = new(position.X, collisionEvent.ajust - collisionBox.bounds.Height); lastCol = "Bottom"; }
                if (collisionEvent.right) { position = new(collisionEvent.ajust - collisionBox.bounds.Width, position.Y); lastCol = "Right"; }
                if (collisionEvent.left) { position = new(collisionEvent.ajust, position.Y); lastCol = "Left"; }
                //position -= velocity;
                Debug.WriteLine(collisionEvent.ajust);
                Debug.WriteLine(position);
                collisionBox.updatePosition(position);
            }
        }
    }
}
