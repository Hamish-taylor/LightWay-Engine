using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay.Engine.ECS.Components
{
    class TransformC
    {

        public Vector2 scale { get; set; } = Vector2.One;

        public Vector2 position { get; set; } = Vector2.Zero;
    }
}
