﻿using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System;


namespace MonoGameTest;
public static class Globals
{
    //public const int SCREEN_WIDTH = 1600;
    //public const int SCREEN_HEIGHT = 900;
    public static float Time { get; private set; }
    public static float ElapsedTime { get; private set; }
    public static bool Slowed { get; set; } = true;
    public static ContentManager Content { get; set; }

    public static GraphicsDevice graphicsDevice { get; set; }
    public static SpriteBatch SpriteBatch { get; set; }

    private static Dictionary<String, Texture2D> textureMap = new();

    public static Texture2D getTextureAndHold(String name)
    {
        if (textureMap.ContainsKey(name))
        {
            return textureMap[name];
        }
        else
        {
            Texture2D texture = Globals.getTexture(name);
            textureMap[name] = texture;
            return texture;
        }
    }
    public static Texture2D getTexture(String name)
    {
        return Globals.Content.Load<Texture2D>(name);
    }

    public static Vector2 getFullScreenSize()
    {
        return new(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
    }
    public static Vector2 getwindowScreenSize()
    {
        return new(graphicsDevice.PresentationParameters.Bounds.Width, graphicsDevice.PresentationParameters.Bounds.Height);
    }

    public static void Update(GameTime gt)
    {
        
    }
}