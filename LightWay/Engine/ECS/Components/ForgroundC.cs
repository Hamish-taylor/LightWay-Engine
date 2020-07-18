using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay.Engine.ECS.Components
{
    /// <summary>
    /// A bad component to fix a dumb problem that i am too lazy to actually fix, oh well 
    /// </summary>
    public class ForgroundC : IComponent
    {
        public Type type { get; } = typeof(ForgroundC);
    }
}
