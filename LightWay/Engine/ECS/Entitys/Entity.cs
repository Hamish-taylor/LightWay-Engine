using System.Collections.Generic;
namespace LightWay
{
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
