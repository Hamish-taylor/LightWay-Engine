using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay.Engine.ECS.Components
{
    class ColorBlockC : IComponent
    {
        public Type type { get; } = typeof(ColorBlockC);

        public Texture2D texture { get; set; }

        public Rectangle bounds { get; private set;}

        public Vector2 scale { get; set; } = new Vector2(0.2f,0.2f);
        public ColorBlockC(Color color,Rectangle bounds,GraphicsDevice graphicsDevice)
        {
            this.bounds = bounds;
            texture = new Texture2D(graphicsDevice, 1, 1);
            Color[] c = new Color[1];
            c[0] = color;
            texture.SetData(c);
        }
    }
}
