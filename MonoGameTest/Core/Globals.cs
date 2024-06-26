﻿using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;




namespace MonoGameTest.Core;
public static class Globals
{
    public static float Time { get; private set; }
    public static float ElapsedTime { get; private set; }
    public static bool Slowed { get; set; } = true;
    public static bool showHitboxes { get; set; } = false;
    public static ContentManager Content { get; set; }
    public static GameWindow gameWindow { get; set; }
    public static GraphicsDevice graphicsDevice { get; set; }
    public static GraphicsDeviceManager graphicsDeviceManager { get; set; }
    public static SpriteBatch SpriteBatch { get; set; }
    public static RenderTarget2D RenderTarget { get; set; }

    private static Dictionary<string, Texture2D> textureMap = new();


    

    public static Texture2D getTextureAndHold(string name)
    {
        if (textureMap.ContainsKey(name))
        {
            return textureMap[name];
        }
        else
        {
            Texture2D texture = getTexture(name);
            textureMap[name] = texture;
            return texture;
        }
    }
    public static Texture2D getTexture(string name)
    {
        return Content.Load<Texture2D>(name);
    }

    public static SpriteFont getSpriteFont(string name)
    {
        return Content.Load<SpriteFont>(name);
    }

    public static Vector2 getFullScreenSize()
    {
        return new(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
    }
    public static Vector2 getwindowScreenSize()
    {
        return new(graphicsDevice.PresentationParameters.Bounds.Width, graphicsDevice.PresentationParameters.Bounds.Height);
    }

    public static void switchToFullScreen(bool isborderless, bool shouldHardwareSwitch)
    {
        gameWindow.Position = new(0, 0);
        gameWindow.IsBorderless = isborderless;
        graphicsDeviceManager.HardwareModeSwitch = shouldHardwareSwitch;
        graphicsDeviceManager.PreferredBackBufferWidth = (int)getFullScreenSize().X;
        graphicsDeviceManager.PreferredBackBufferHeight = (int)getFullScreenSize().Y;
        graphicsDeviceManager.ApplyChanges();
    }


    public static void Update(GameTime gt)
    {

    }
}
