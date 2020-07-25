using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace LightWay
{
    /// <summary>
    /// Probably gonna get removed. Stores the id of an entity and all of its components.
    /// Its not currently used or nessesary in the current build
    /// </summary>
    public class Entity
    {
       
        public List<Type> components { get; set; } = new List<Type>();

        public static implicit operator int(Entity e) => e.id;

        /// <summary>
        /// The entity controller that this entity belongs to
        /// </summary>
        public EntityController entityController { get; private set; }

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

        public Entity(int id,EntityController entityController)
        {
            this.entityController = entityController;
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




        //METHODS I WOULD LIKE
        public T GetComponent<T>()
        {
            if(entityController.TryGetComponent<T>(this,out T component))
            {
                return component;
            }

            throw new EntityException("Entity does not contain component");

        }
    }

    [Serializable]
    internal class EntityException : Exception
    {
        private object p;

        public EntityException()
        {
        }

        public EntityException(object p)
        {
            this.p = p;
        }

        public EntityException(string message) : base(message)
        {
        }

        public EntityException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EntityException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
