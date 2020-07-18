using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay.Engine.ECS.Components
{
    public class GravityCDEPRECATED : IComponent
    {
        public Type type { get; } = typeof(GravityCDEPRECATED);

        public Vector2 gravity = new Vector2(0, 0);

        public GravityCDEPRECATED(Vector2 gravity)
        {
            this.gravity = gravity;
        }
    }
}
