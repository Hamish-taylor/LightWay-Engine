using System;
using System.Collections.Generic;

namespace LightWay
{
    class ComponentIndexPool
    {
        private Dictionary<Type, Dictionary<int, IComponent>> pool { get; set; } = new Dictionary<Type, Dictionary<int, IComponent>>();

        public Dictionary<int, IComponent> GetAll(Type type)
        {
            Dictionary<int, IComponent> d = new Dictionary<int,IComponent>();
            if(pool.ContainsKey(type))
            {
                pool.TryGetValue(type,out d);
            }
            return d;
        } 

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
