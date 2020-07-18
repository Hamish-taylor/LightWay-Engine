using System;
using System.Collections.Generic;
namespace LightWay
{
    /// <summary>
    /// Probably gonna get removed. Stores the id of an entity and all of its components.
    /// Its not currently used or nessesary in the current build
    /// </summary>
    public class Entity : IEntity
    {
       
        public List<IComponent> components { get; set; } = new List<IComponent>();
        public int id { get; private set; }
        private EntityGroup _entityGroup = null;
        public EntityGroup EntityGroup 
        { 
            get => _entityGroup; 
            set 
            { 
                _entityGroup.entities.Remove(this); 
                value.entities.Add(this); 
                _entityGroup = value; 
            } 
        }

        private bool _active = true;
        public bool Active 
        {
            get 
            {
                if(_entityGroup != null)
                {
                    return _entityGroup.active;
                }else
                {
                    return _active;
                }
            }
            set 
            { 
                if (_entityGroup != null) throw new Exception("Cannot change activity of a grouped entity"); 
                else _active = value; 
            } 
        }

        public Entity(int id)
        {
            this.id = id;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return ((Entity)obj).id == this.id;
        }

        public override int GetHashCode()
        {
            return id;
        }
    }
}
