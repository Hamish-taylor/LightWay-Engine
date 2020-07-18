using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay.Engine.ECS.Components
{
    public class VelocityC : IComponent
    {

        public static implicit operator Vector2(VelocityC v) => v.velocity;

        public static explicit operator VelocityC(Vector2 v) => new VelocityC(v);


        public Type type { get; } = typeof(VelocityC);

        public Vector2 velocity = new Vector2(0, 0);

        public VelocityC(Vector2 velocity) {
            this.velocity = velocity;
        }

        public VelocityC()
        {
        }
    }
}
