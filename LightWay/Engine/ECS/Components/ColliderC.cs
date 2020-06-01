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
    }
}
