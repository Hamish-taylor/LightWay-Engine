using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay.Engine.ECS.Components
{
    public class TextureC : IComponent
    {
        public Texture2D Texture { get; set; }
        public Type type { get; } = typeof(TextureC);

        public static implicit operator Texture2D(TextureC t) => t.Texture;

        /// <summary>
        /// Allows explicit conversion between Texture2d and TextureC. This will create the texture with a scale of one 
        /// </summary>
        /// <param name="t"></param>
        public static explicit operator TextureC(Texture2D t) => new TextureC(t,1);
        public Vector2 scale { get; set; } = new Vector2(0.2f, 0.2f);

        public long id { get; private set; } = 1;

        /// <summary>
        /// Create component from a texture
        /// </summary>
        /// <param name="texture">The texture of the component</param>
        /// <param name="scale">The scale to render the texture (Percent of the original)</param>
        public TextureC(Texture2D texture, Vector2 scale)
        {
            this.scale = scale;
            this.Texture = texture;
            this.id = (long)Math.Pow(2, id);
;        }
        /// <summary>
        /// Create component from a texture with a square scale
        /// </summary>
        /// <param name="texture">The texture of the component</param>
        /// <param name="scale">The scale to render the texture, This will be set for both width and height</param>
        public TextureC(Texture2D texture, float scale)
        {
            this.scale = new Vector2(scale,scale);
            this.Texture = texture;
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
            Texture = new Texture2D(graphicsDevice, 1, 1);
            Color[] c = new Color[1];
            c[0] = color;
            Texture.SetData(c);
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
            Texture = new Texture2D(graphicsDevice, 1, 1);
            Color[] c = new Color[1];
            c[0] = color;
            Texture.SetData(c);
        }
    }
}
