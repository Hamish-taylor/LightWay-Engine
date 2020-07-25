using System;
using System.Collections.Generic;
using System.Linq;

namespace LightWay
{
    /// <summary>
    /// Stores a Pool of components, Aswell as helper methods to add and get
    /// </summary>
    public class ComponentIndexPool
    {
        private Dictionary<Type, Dictionary<Entity, object>> pool { get; set; } = new Dictionary<Type, Dictionary<Entity, object>>();
        private Dictionary<Entity, object> d;

        /// <summary>
        /// Returns all components in the pool of a given type.
        /// </summary>
        /// <param name="type"> The type of component you want to retreve</param>
        /// <returns>A dictonary of components with their entity ID's as keys</returns>
        public Dictionary<Entity, object> GetAll(Type type)
        {
            d = new Dictionary<Entity, object>();
            d = pool[type];       
            return d;
        }

        public List<Entity> GetAllEntityWithComponent<T>()
        {
            return pool[typeof(T)].Keys.ToList();
        }


        public T Get<T>(Entity id)
        {            
            if(pool.ContainsKey(typeof(T)) && pool[typeof(T)].ContainsKey(id))
            return (T)pool[typeof(T)][id];          
            return default;
        }
        /// <summary>
        /// Inserts a component into the pool
        /// </summary>
        /// <param name="component"> Component to insert</param>
        /// <param name="index">The ID of the entity that this component belongs to</param>
        public void InsertComponent<T>(T component,Entity index)
        {
            Type type = component.GetType();
            if (pool.ContainsKey(type))
            {
                if (pool[type].ContainsKey(index)) pool[type][index] = component;
                 else
                {
                    pool[type].Add(index, component);
                }
            }else
            {
                Dictionary<Entity, object> d = new Dictionary<Entity, object>();
                d.Add(index,component);
                pool.Add(type, d);
            }
        }
    }
}
