using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay.Engine.ECS.Components
{
    public class TextureC 
    {
        public Texture2D Texture { get; set; }

        public static implicit operator Texture2D(TextureC t) => t.Texture;

        /// <summary>
        /// Allows explicit conversion between Texture2d and TextureC. This will create the texture with a Scale of one 
        /// </summary>
        /// <param name="t"></param>
        public static explicit operator TextureC(Texture2D t) => new TextureC(t);

        /// <summary>
        /// Create component from a texture
        /// </summary>
        /// <param name="texture">The texture of the component</param>
        /// <param name="Scale">The Scale to render the texture (Percent of the original)</param>
        public TextureC(Texture2D texture)
        {
            this.Texture = texture;
;        }

        /// <summary>
        /// Create component from a base color
        /// </summary>
        /// <param name="graphicsDevice">The games GraphicsDevice</param>
        /// <param name="Scale">The Scale to render the texture (Pixels)</param>
        /// <param name="color">The base color of the texture</param>
        public TextureC(GraphicsDevice graphicsDevice, Color color)
        {
            Texture = new Texture2D(graphicsDevice, 1, 1);
            Color[] c = new Color[1];
            c[0] = color;
            Texture.SetData(c);
        }
    }
}
