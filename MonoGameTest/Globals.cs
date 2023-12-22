


using Microsoft.Xna.Framework.Content;


namespace MonoGameTest;
public static class Globals
{
    public const int SCREEN_WIDTH = 1600;
    public const int SCREEN_HEIGHT = 900;
    public static float Time { get; private set; }
    public static float ElapsedTime { get; private set; }
    public static bool Slowed { get; set; } = true;
    public static ContentManager Content { get; set; }
    public static SpriteBatch SpriteBatch { get; set; }

    public static void Update(GameTime gt)
    {
        
    }
}
