using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay
{
    class Texture : IComponent
    {
        public Texture2D texture { get; set; }
        public Type type { get; private set; }

        public Texture(Texture2D texture)
        {   
            this.texture = texture;
            this.type = this.GetType();
        }
    }
}
