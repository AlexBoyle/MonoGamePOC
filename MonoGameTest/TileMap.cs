using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameTest
{
    internal class TileMap
    {
        Texture2D tileMapSource;
        protected Color color = Color.White;
        protected float rotation;
        public int tileSize = 16;
        Vector2 origin;
        Random rand;
        int[,] tileType  = new int[32,32];

        public TileMap()
        {
            tileMapSource = GlobalAssetManager.getTexture("StardewTileMap");
            origin = new(16, 16);
            rand = new Random();
            for (var x = 0; x < 32; x++)
            {
                for (var y = 0; y < 32; y++)
                {
                    tileType[x, y] = rand.Next(0, 4);
             
                }
            }
        }

        public void draw()
        {
            Rectangle[] grass = { getSprite(0, 7), getSprite(0, 6), getSprite(1, 6), getSprite(2, 6) };
            for (var x = 0; x < 32; x ++)
            {
                for (var y = 0; y < 32; y++)
                {
                    Vector2 position = new Vector2(x* tileSize, y* tileSize);
                    Globals.SpriteBatch.Draw(tileMapSource, position, grass[tileType[x,y]], color, rotation, origin, 1, SpriteEffects.None, 1);
                }
            }

        }

        private Rectangle getSprite(int x, int y)
        {
            return new Rectangle(tileSize * x, tileSize * y, tileSize, tileSize);
        }
    }
}
