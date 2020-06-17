using System;
using System.Collections.Generic;

namespace LightWay
{
    /// <summary>
    /// Stores a Pool of components, Aswell as helper methods to add and get
    /// </summary>
    public class ComponentIndexPool
    {
        private Dictionary<Type, Dictionary<int, IComponent>> pool { get; set; } = new Dictionary<Type, Dictionary<int, IComponent>>();
        private Dictionary<int, IComponent> d;

        /// <summary>
        /// Returns all components in the pool of a given type.
        /// </summary>
        /// <param name="type"> The type of component you want to retreve</param>
        /// <returns>A dictonary of components with their entity ID's as keys</returns>
        public Dictionary<int, IComponent> GetAll(Type type)
        {
            d = new Dictionary<int,IComponent>();
            d = pool[type];       
            return d;
        }

        public IComponent Get(Type type,int id)
        {            
            if(pool.ContainsKey(type) && pool[type].ContainsKey(id))
            return pool[type][id];          
            return null;
        }
        /// <summary>
        /// Inserts a component into the pool
        /// </summary>
        /// <param name="component"> Component to insert</param>
        /// <param name="index">The ID of the entity that this component belongs to</param>
        public void InsertComponent(IComponent component,int index)
        {
            Type type = component.type;
            if (pool.ContainsKey(type))
            {
                if (pool[type].ContainsKey(index)) pool[type][index] = component;
                 else
                {
                    pool[type].Add(index, component);
                }
            }else
            {
                Dictionary<int, IComponent> d = new Dictionary<int,IComponent>();
                d.Add(index,component);
                pool.Add(type, d);
            }
        }
    }
}
