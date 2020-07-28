using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay.Engine.ECS.Tools
{
    public static class TextHelper
    {

        public static Dictionary<string, Font> fonts = new Dictionary<string, Font>();

        /// <summary>
        /// Creates a font from a texture, Warning it currently preforms no checks on the passed in texture so make sure it follows guide lines first 
        /// </summary>
        /// <param name="texture">The master texture for the font</param>
        /// <param name="graphicsDevice">The graphics device</param>
        public static void CreateFont(Texture2D texture,string name,GraphicsDevice graphicsDevice)
        {
            Font f = new Font(texture,graphicsDevice,name);
            fonts.Add(name, f);
        }

        public static Texture2D GenerateFontTexture(string text,string font, Color color, int wordPixelSpacing = 2,int letterPixelSpacing = 1)
        {
            return fonts[font].CreateTextureFromString(text, wordPixelSpacing,letterPixelSpacing,color);
        }

        public static Texture2D GenerateFontTexture(string text, string font, int wordPixelSpacing = 2, int letterPixelSpacing = 1)
        {
            return fonts[font].CreateTextureFromString(text, wordPixelSpacing, letterPixelSpacing, Color.Black);
        }
    }
}
