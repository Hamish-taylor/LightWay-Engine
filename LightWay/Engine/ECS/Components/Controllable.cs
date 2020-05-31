using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay
{
    class Controllable : IComponent
    {
        public Type type { get; } = typeof(Controllable);
    }
}
