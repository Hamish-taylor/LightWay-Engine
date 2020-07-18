using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay
{
    /// <summary>
    /// The base interface that all Components must inherit from.
    /// Naming Convention from components is to end the name with a capital c eg MyComponentC
    /// </summary>
    public interface IComponent
    {
        /// <summary>
        /// The type of the current component, This is used to easier sorting
        /// </summary>
        Type type { get; }

    }
}
