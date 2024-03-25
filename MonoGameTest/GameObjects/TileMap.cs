using System;
using System.Collections.Generic;

namespace MonoGameTest.GameObjects
{
    internal class TileMap : GameObject
    {

        Texture2D tileMapSource;
        protected Color color = Color.White;
        protected float rotation;
        public int tileSize = 16;
        Vector2 origin;
        Random rand;
        int[,] tileType = new int[32, 32];
        List<CollisionBox> collisionBoxes = new List<CollisionBox>();

        public TileMap()
        {
            

            tileMapSource = Globals.getTextureAndHold("SpriteMaps/StardewTileMap");
            origin = new(16, 16);
            rand = new Random();
            int id = 0;
            for (var x = 0; x < 32; x++)
            {
                for (var y = 0; y < 32; y++)
                {
                    tileType[x, y] = rand.Next(0, 4);
                    if ((x == 1 && y > 2 && y < 29) ||(x == 28 && y > 2 && y < 29) || (y == 3 && x > 1 && x < 28) || (y == 29 && x >1 && x < 28))
                    {
                        id++;
                        CollisionBox collisionBox = new(this, new(new(x * tileSize, y * tileSize), new(16, 16)));
                        collisionBox.id = id;
                        collisionBoxes.Add(collisionBox);
                        CollisionMaster.registerCollisionBox(collisionBox);
                    }

                }
            }
        }

        public override void update(GameTime gameTime) { }

        public override void draw(GameTime gameTime)
        {
            Rectangle[] wall = {getSprite(272, 288, 16, 64), getSprite(304, 256, 16, 16), getSprite(256, 256, 16, 16), getSprite(211, 256, 16, 16),
                getSprite(192, 256, 16, 16), getSprite(240, 256, 16, 16), getSprite(288, 224, 16, 64), getSprite(320, 288, 16, 64)
            };
            Rectangle[] grass = { getSprite(0, 7), getSprite(0, 6), getSprite(1, 6), getSprite(2, 6) };
            for (var x = 0; x < 32; x++)
            {
                for (var y = 0; y < 32; y++)
                {
                    Vector2 position = new Vector2(x * tileSize, y * tileSize);
                    Globals.SpriteBatch.Draw(tileMapSource, position, grass[tileType[x, y]], color, rotation, origin, 1, SpriteEffects.None, .3f);
                }
            }
            for (var x = 0; x < 32; x++)
            {
                for (var y = 0; y < 32; y++)
                {
                    bool shouldDraw = false;
                    float layer = .2f;
                    int side = -1;
                    if (x == 2 && y > 2 && y < 29)
                    {
                        side = 1;
                    }
                    if (x == 29 && y > 2 && y < 29)
                    {
                        side = 2;
                    }
                    if (y == 2 && x > 2 && x < 29)
                    {
                        side = 0;
                        layer += .05f;
                    }
                    if (y == 29 && x > 2 && x < 29)
                    {
                        side = 3;
                        layer = .05f;
                    }
                    if (y == 29 && x == 2)
                    {
                        side = 4;
                        
                    }
                    if (y == 29 && x == 29)
                    {
                        side = 5;
                    }
                    if (y == 2 && x == 3)
                    {
                        side = 6;

                    }
                    if (y == 2 && x == 28)
                    {
                        side = 7;

                    }
                    if (side >= 0)
                    {
                        Vector2 position = new Vector2(x * tileSize, y * tileSize);
                        Globals.SpriteBatch.Draw(tileMapSource, position, wall[side], color, rotation, origin, 1, SpriteEffects.None, layer);
                        
                    }
                }
            }
            if (Globals.showHitboxes)
            {
                foreach (CollisionBox col in collisionBoxes)
                {
                    Globals.SpriteBatch.Draw(DebugResorce.whiteRectangle, col.bounds, Color.Chocolate);
                }
            }
            

        }

        private Rectangle getSprite(int x, int y)
        {
            return new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);
        }

        private Rectangle getSprite(int x, int y, int tileSizeX, int tileSizeY)
        {
            return new Rectangle(x, y, tileSizeX, tileSizeY);
        }
    }
}
