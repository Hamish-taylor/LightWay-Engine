using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay
{
    /// <summary>
    /// The base interface that all Entitys must inherit from
    /// </summary>
    public interface IEntity
    {
        int id { get; }
        List<IComponent> components { get; set; }
    }
}
