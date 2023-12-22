using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameTest
{
    internal class GlobalAssetManager
    {
        public static Dictionary<String, Texture2D> textureMap = new();

        public static Texture2D getTexture(String name)
        {
            if(textureMap.ContainsKey(name))
            {
                return textureMap[name];
            }
            else
            {
                Texture2D texture = Globals.Content.Load<Texture2D>(name);
                textureMap[name] = texture;
                return texture;
            }
        }
        

    }
}
