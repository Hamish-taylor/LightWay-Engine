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
        public Type type { get; private set; }

        public TextureC(Texture2D texture)
        {   
            this.texture = texture;
            this.type = this.GetType();
        }
    }
}
