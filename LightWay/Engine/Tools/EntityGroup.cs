using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay
{
    public class EntityGroup
    {

        public HashSet<Entity> entities { get; private set; } = new HashSet<Entity>();
        private bool _active = true;
        public bool active 
        { 
            get 
            { 
                return _active; 
            } 
            set 
            {
                _active = value;
            } 
        }

        public void Add(Entity entity)
        {
            entities.Add(entity);
        }

        public void Remove(Entity entity)
        {
            entities.Remove(entity);
        }

    }
}
