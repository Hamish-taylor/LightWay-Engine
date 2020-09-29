using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay.Engine.Tools
{
    class TextureBuilder
    {
        public static Texture2D Rectangle(int width, int height, Color color)
        {
            Texture2D texture = new Texture2D(Game1.graphicsDevice, width, height);
            Color[] colors = new Color[width*height];
            for (int i = 0; i < colors.Length; i++) colors[i] = color;
            texture.SetData(colors);
            return texture;
        }
        //BROKEN
        public static Texture2D combineTextures(Texture2D bottom,Texture2D top,int offsetX = 0,int offsetY = 0)
        {
            int width = top.Width + offsetX > bottom.Width ? top.Width + offsetX : bottom.Width;
            int height = top.Height + offsetY > bottom.Height ? top.Height + offsetY : bottom.Height;

            Texture2D output = new Texture2D(Game1.graphicsDevice, width, height);

            Color[] bottomColors = new Color[bottom.Width * bottom.Height];
            bottom.GetData(bottomColors);

            Color[] topColors = new Color[top.Width * top.Height];
            top.GetData(topColors);

            Color[] colors = new Color[width * height];

            for(int h = 0; h < height-1; h++)
            {
                for(int w = 0; w < width-1; w++)
                {
                    colors[h * width + w] = new Color(bottomColors[h * width + w].ToVector4() + topColors[h * width + w].ToVector4());
                }

            }

            output.SetData(colors);
            return output;
        }
        
        
    }
}
