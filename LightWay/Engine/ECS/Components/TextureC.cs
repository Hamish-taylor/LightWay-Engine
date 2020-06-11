using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay
{
    class TextureC : IComponent
    {
        public Texture2D texture { get; set; }
        public Type type { get; } = typeof(TextureC);

        public Vector2 scale { get; set; } = new Vector2(0.2f, 0.2f);
        /// <summary>
        /// Create component from a texture
        /// </summary>
        /// <param name="texture">The texture of the component</param>
        /// <param name="scale">The scale to render the texture (Percent of the original)</param>
        public TextureC(Texture2D texture, Vector2 scale)
        {
            this.scale = scale;
            this.texture = texture;
        }
        /// <summary>
        /// Create component from a base color
        /// </summary>
        /// <param name="graphicsDevice">The games GraphicsDevice</param>
        /// <param name="scale">The scale to render the texture (Pixels)</param>
        /// <param name="color">The base color of the texture</param>
        public TextureC(GraphicsDevice graphicsDevice, Vector2 scale, Color color)
        {
            this.scale = scale;
            texture = new Texture2D(graphicsDevice, 1, 1);
            Color[] c = new Color[1];
            c[0] = color;
            texture.SetData(c);
        }

        /// <summary>
        /// Create component from a base color
        /// </summary>
        /// <param name="graphicsDevice">The games GraphicsDevice</param>
        /// <param name="width">Width of the texture</param>
        /// <param name="height">Height of the texture</param>
        /// <param name="color">Color of the texture</param>
        public TextureC(GraphicsDevice graphicsDevice, float width,float height, Color color)
        {
            this.scale = new Vector2(width,height);
            texture = new Texture2D(graphicsDevice, 1, 1);
            Color[] c = new Color[1];
            c[0] = color;
            texture.SetData(c);
        }
    }
}
