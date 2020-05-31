using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay.Engine.ECS.Components
{
    class GravityC : IComponent
    {
        public Type type { get; } = typeof(GravityC);

        public Vector2 gravity = new Vector2(0, 0);

        public GravityC(Vector2 gravity)
        {
            this.gravity = gravity;
        }
    }
}
