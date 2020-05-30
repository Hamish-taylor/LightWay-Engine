using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay
{
    abstract class System : ISystem
    {
        public List<Type> components { get; set; } = new List<Type>();

        public Dictionary<Type, IComponent> workingEntity { get; private set; } = new Dictionary<Type, IComponent>();
       
        public virtual void update(GameTime gameTime, ComponentIndexPool CIP)
        {
            //Looping through a key set of position components
            foreach (var p in CIP.GetAll(components[0]))
            {
                //get the first component
                IComponent first = p.Value;
                //get its entity id
                int id = p.Key;
                //see if the other components in components also contain a components for this entity
                bool foundAll = true;
                for (int i = 1; i < components.Count; i++)
                {
                    IComponent c = null;
                    if (!CIP.GetAll(components[i]).TryGetValue(id, out c))
                    {
                        foundAll = false;
                        break;
                    }
                    //if the component is attached to the entity add it to the working entity 
                    workingEntity[components[i]] = c;
                }
                if (foundAll)
                {
                    workingEntity[components[0]] = first;
                    ProcessEntity();
                }
            }
        }
        public virtual void ProcessEntity()
        {
        }
        public virtual void Init()
        {
            foreach (Type t in components)
            {
                workingEntity.Add(t, null);
            }
        }
    }
}
