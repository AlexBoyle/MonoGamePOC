using System.Globalization;
using System.Collections.Generic;
using MonoGameTest.Physics;
using MonoGameTest.Core;
namespace MonoGameTest.GameObjects
{
    internal class Alex : Sprite
    {
        int pointCycle = 0;
        float deltaSinceSpriteUpdate = 0;
        int direction = 0;
        CollisionBox collisionBox;
        Vector2 collisionOffset = new(0, 16);
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
            collisionBox.updatePosition(position + collisionOffset);
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
                collisionBox.updatePosition(position + collisionOffset);
            }
        }

        string formatFloat(float f)
        {
            return f.ToString("###,0.00", numberFormat);
        }

        public override void draw(GameTime gameTime)
        {
            //Globals.SpriteBatch.DrawString( Globals.getSpriteFont("fonts/default"), "Vel: " + formatFloat(velocity.X) + " : " + formatFloat(velocity.Y), new Vector2(collisionBox.bounds.X, collisionBox.bounds.Y - 90), Color.Black);
            //Globals.SpriteBatch.DrawString( Globals.getSpriteFont("fonts/default"), "LastCol: " + lastCol, new Vector2(collisionBox.bounds.X, collisionBox.bounds.Y - 75), Color.Black);
            //Globals.SpriteBatch.DrawString( Globals.getSpriteFont("fonts/default"), "x: "  + formatFloat(position.X) + " :" + formatFloat(position.Y), new Vector2(collisionBox.bounds.X, collisionBox.bounds.Y - 60), Color.Black);
            //Globals.SpriteBatch.DrawString( Globals.getSpriteFont("fonts/default"), "x: "  + collisionBox.bounds.X + "      :" + collisionBox.bounds.Y, new Vector2(collisionBox.bounds.X, collisionBox.bounds.Y - 45), Color.Black);
            if (Globals.showHitboxes) { Globals.SpriteBatch.Draw(Globals.getWhite(), collisionBox.bounds, Color.Aqua); }
            base.draw(gameTime);
        }

        public override void resolveEvents(Event evt, object d)
        {
            if (evt == Event.Collision)
            {
                CollisionEvent collision = new((List<CollisionEvent>)d);
                resolveEvent(evt, collision);
            }
        }

        public override void resolveEvent(Event evt, object d) { 
            if(evt == Event.Collision)
            {
                CollisionEvent collisionEvent = (CollisionEvent)d;
                //Debug.WriteLine(collisionEvent.toString());
                if (collisionEvent.top) {
                    float diff = position.Y - velocity.Y;
                    if ((int)diff != collisionEvent.allignToTop) { 
                        diff = collisionEvent.allignToTop - collisionOffset.Y; 
                    }
                    position = new(position.X, diff); 
                    lastCol = "Top"; 
                }
                if (collisionEvent.bottom) {
                    float diff = position.Y - velocity.Y;
                    if ((int)diff + collisionBox.bounds.Height + collisionOffset.Y != collisionEvent.allignToBottom) {
                        diff = collisionEvent.allignToBottom - collisionBox.bounds.Height - collisionOffset.Y; 
                    }
                    position = new(position.X, diff);
                    lastCol = "Bottom"; 
                }
                if (collisionEvent.right) {
                    float diff = position.X - velocity.X;
                    if ((int)diff + collisionBox.bounds.Width != collisionEvent.allignToRight) { 
                        diff = collisionEvent.allignToRight - collisionBox.bounds.Width; 
                    }
                    position = new(diff, position.Y); 
                    lastCol = "Right"; 
                }
                if (collisionEvent.left) {
                    float diff = position.X - velocity.X;
                    if ((int)diff != collisionEvent.allignToLeft) { 
                        diff = collisionEvent.allignToLeft; 
                    }
                    position = new(diff, position.Y); 
                    lastCol = "Left"; 
                }
                collisionBox.updatePosition(position + collisionOffset);
            }
        }
    }
}
