using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay
{
    /// <summary>
    /// The base class all Systems must extend
    /// </summary>
    public abstract class System : ISystem
    {
        /// <summary>
        /// A list of the components the system operates on
        /// </summary>
        public List<Type> components { get; set; } = new List<Type>();
        /// <summary>
        /// Stores the current components that are being worked on
        /// </summary>
        public Dictionary<Type, IComponent> workingEntity = new Dictionary<Type, IComponent>();
       
        public System()
        {

        }
        /// <summary>
        /// The main update method. 
        /// It finds a set of aplicable components and then adds them to the working entity
        /// </summary>
        /// <param name="gameTime">The games <c>GameTime</c></param>
        /// <param name="CIP">The ComponentIndexPool you want to retreve your entitys from</param>
        public virtual void update(GameTime gameTime, ComponentIndexPool CIP)
        {
            IComponent first;
            //Looping through a key set of position components
            foreach (var p in CIP.GetAll(components[0]))
            {
                //get the first component
                first = p.Value;
                //get its entity id
                int id = p.Key;
                //see if the other components in components also contain a components for this entity
                bool foundAll = true;
                for (int i = 1; i < components.Count; i++)
                {
                    IComponent c = CIP.Get(components[i], id);
                    if (c == null)
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
        /// <summary>
        /// Where your system specific processing code belongs 
        /// </summary>
        public abstract void ProcessEntity();
        /// <summary>
        /// Populates the working entity with the apropriate types
        /// </summary>
        public virtual void Init()
        {
            foreach (Type t in components)
            {
                workingEntity.Add(t, null);
            }
        }
        /// <summary>
        /// Used to get a component from the working entity
        /// </summary>
        /// <typeparam name="T">The Type of the component. Must be of type IComponent</typeparam>
        /// <returns>The found component </returns>
        public T GetComponent<T>() where T: IComponent
        {
            return ((T)workingEntity[typeof(T)]);
        }

        internal class Drawing
        {
        }
    }
}
