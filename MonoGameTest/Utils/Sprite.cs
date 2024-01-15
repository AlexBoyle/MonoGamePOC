using System.Security.Cryptography.Xml;
using MonoGameTest.Core;

namespace MonoGameTest.Utils;
public class Sprite : GameObject
{
    protected Texture2D texture = null;
    protected Vector2 origin = Vector2.Zero;
    protected float rotation = 0f;
    protected Color color = Color.White;
    protected int spriteSizeX = 16;
    protected int spriteSizeY = 32;
    protected int spriteOffsetX = 21;
    protected int spriteOffsetY = 13;
    protected float zIndex = .1f;
    protected Rectangle spriteIndex = new();

    public void setTexture(Texture2D texture)
    {
        origin = new(texture.Width / 2, texture.Height / 2);
        spriteIndex = new Rectangle(spriteOffsetX, spriteOffsetY, spriteSizeX, spriteSizeY);
    }

    public void setSprite(int spriteX, int spriteY)
    {
        spriteIndex = new Rectangle(spriteOffsetX + spriteX * spriteSizeX, spriteOffsetY + spriteY * spriteSizeY, spriteSizeX, spriteSizeY);
    }

    public override void draw(GameTime gameTime)
    {
        Globals.SpriteBatch.Draw(texture, position, spriteIndex, color, rotation, Vector2.Zero, 1, SpriteEffects.None, zIndex);
    }
}