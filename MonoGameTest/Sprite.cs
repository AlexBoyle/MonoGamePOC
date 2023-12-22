using System.Security.Cryptography.Xml;

namespace MonoGameTest;
public class Sprite
{
    protected readonly Texture2D texture;
    protected readonly Vector2 origin;
    public Vector2 position;
    public bool Dead { get; set; }
    protected float rotation;
    protected Color color = Color.White;
    protected int spriteSizeX = 16;
    protected int spriteSizeY = 32;
    protected int spriteOffsetX = 21;
    protected int spriteOffsetY = 13;
    protected Rectangle spriteIndex;

    public Sprite(Texture2D tex, Vector2 pos)
    {
        texture = tex;
        position = pos;
        origin = new(tex.Width / 2, tex.Height / 2);
        spriteIndex = new Rectangle(spriteOffsetX, spriteOffsetY, spriteSizeX, spriteSizeY);
    }

    public void setSprite(int spriteX, int spriteY)
    {
        spriteIndex = new Rectangle(spriteOffsetX + (spriteX * spriteSizeX), spriteOffsetY + (spriteY * spriteSizeY), spriteSizeX, spriteSizeY);
    }

    public virtual void Draw()
    {
        Globals.SpriteBatch.Draw(texture, position, spriteIndex, color, rotation, Vector2.Zero, 1, SpriteEffects.None, 1);
    }
}