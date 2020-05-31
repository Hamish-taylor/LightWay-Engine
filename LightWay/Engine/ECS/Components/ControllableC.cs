using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay
{
    class ControllableC : IComponent
    {
        public Type type { get; } = typeof(ControllableC);
    }
}
