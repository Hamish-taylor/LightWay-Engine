using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay.Engine.ECS.Components
{
    class ColliderC : IComponent
    {
        public Type type { get; } = typeof(ColliderC);

        public Rectangle collider = new Rectangle();

        public Vector4 colDir { get; set; } = new Vector4();

        public ColliderC(Rectangle collider)
        {
            this.collider = collider;
        }
        public ColliderC(int x,int y,int width, int height)
        {
            this.collider = new Rectangle(x,y,width,height);
        }

    }
}
