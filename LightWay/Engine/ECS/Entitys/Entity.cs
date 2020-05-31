using System.Collections.Generic;
namespace LightWay
{
    /// <summary>
    /// Probably gonna get removed. Stores the id of an entity and all of its components.
    /// Its not currently used or nessesary in the current build
    /// </summary>
    class Entity : IEntity
    {
        public List<IComponent> components { get; set; } = new List<IComponent>();
        public int id { get; private set; }

        public Entity(int id)
        {
            this.id = id;
        }
    }
}
