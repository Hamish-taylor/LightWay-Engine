using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay.Engine.ECS.Components
{
    class VelocityC : IComponent
    {
        public Type type { get; } = typeof(VelocityC);

        public Vector2 velocity = new Vector2(0, 0);
    }
}
