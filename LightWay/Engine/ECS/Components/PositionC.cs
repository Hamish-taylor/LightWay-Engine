using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay
{
    class PositionC : IComponent
    {
        public Vector2 position;
        public Type type { get; private set; } 

        public PositionC(Vector2 position)
        {
            this.position = position;
            this.type = typeof(PositionC);
        }

        public PositionC(float x, float y)
        {
            this.position = new Vector2(x,y);
            this.type = typeof(PositionC);
        }
    }
}
