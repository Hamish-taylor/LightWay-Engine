using LightWay.Engine.ECS.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay.Engine.ECS.Systems
{
    /// <summary>
    /// For now this system cashes all collider objects in the game
    /// This assumes that a collider will never be removed from a entity
    /// </summary>
    class CollisionSystem : System
    {

        private int[] moveableEntitys;
        private int[] staticEntitys;

        private List<ColliderC> wasCollided = new List<ColliderC>();
        public bool start { get; set; }

        private ComponentIndexPool CIP;
        public CollisionSystem(ComponentIndexPool CIP)
        {
            this.CIP = CIP;
            components.Add(typeof(ColliderC));
            components.Add(typeof(PositionC));
            components.Add(typeof(VelocityC));
            start = true;
            Init();
        }
        public override void update(GameTime gameTime, ComponentIndexPool CIP)
        {
            if(moveableEntitys == null || staticEntitys == null)
            {
                UpdateEntitys(CIP);
            }
            
            ProcessEntity();
        }
        public override void ProcessEntity()
        {
            foreach (ColliderC c in wasCollided)
            {
                c.colDir = Vector4.Zero;
            }
            foreach (int move in moveableEntitys)
            {
                ColliderC movable = ((ColliderC)CIP.Get(typeof(ColliderC), move));
                Vector2 pos = movable.collider.Location.ToVector2();
                movable.collider.Location = ((PositionC)CIP.Get(typeof(PositionC), move)).position.ToPoint();
                foreach (int stat in staticEntitys)
                {
                    ColliderC st = ((ColliderC)CIP.Get(typeof(ColliderC), stat));
                    if (movable.collider.Intersects(st.collider))
                    {
                        ((PositionC)CIP.Get(typeof(PositionC), move)).position = pos;

                        wasCollided.Add(movable);
                        movable.colDir = Vector4.Zero;
                        movable.colDir += CollisionDirection(movable.collider, st.collider);
                    }
                }
            }
        }

        /// <summary>
        /// Finds the side of collider one collider 2 is on and returns that in a vector 4, Vector4(Top,Right,Bottom,Left)
        /// </summary>
        /// <param name="collider1"></param>
        /// <param name="collider2"></param>
        /// <returns></returns>
        public Vector4 CollisionDirection(Rectangle collider1, Rectangle collider2)
        {
            Vector4 vec = new Vector4();
            /*if (collider2.Bottom > collider1.Top) vec.X = 1;
            if (collider2.Right < collider1.Left) vec.W = 1;
            if (collider2.Top < collider1.Bottom) vec.Z = 1;
            if (collider2.Left > collider1.Right) vec.Y = 1;*/
            if (collider2.Bottom < collider1.Top && collider2.Top > collider1.Top && !(collider2.Top < collider1.Bottom && collider2.Bottom > collider1.Bottom)) vec.X = 1;
            if (collider2.Right > collider1.Left && collider2.Left < collider1.Left && !(collider2.Left < collider1.Right && collider2.Right > collider1.Right)) vec.W = 1;
            if (collider2.Top < collider1.Bottom && collider2.Bottom > collider1.Bottom && !(collider2.Bottom < collider1.Top && collider2.Top > collider1.Top)) vec.Z = 1;
            if (collider2.Left < collider1.Right && collider2.Right > collider1.Right && !(collider2.Right > collider1.Left && collider2.Left < collider1.Left)) vec.Y = 1;
            return vec;
        }

        public void UpdateEntitys(ComponentIndexPool CIP)
        {
            List<int> moveableEntitysL = new List<int>();
            List<int> staticEntitysL = new List<int>();
            if (start == true)
            {
                start = false;
                UpdateEntitys(CIP);
            }
            IComponent first;
            IComponent c = null;
            //Looping through a key set of position components
            foreach (var p in CIP.GetAll(components[0]))
            {
                //get the first component
                first = p.Value;
                //get its entity id
                int id = p.Key;
                //see if the other components in components also contain a components for this entity
                bool foundAll = true;
                bool partial = true;
                for (int i = 1; i < components.Count; i++)
                {
                    c = CIP.Get(components[i], id);
                    if (c == null)
                    {
                        if (i == components.Count - 1) {
                            partial = true;
                            foundAll = false;
                        }
                        else
                        {
                            foundAll = false;
                            partial = false;
                            break;
                        }   
                    }
                    //if the component is attached to the entity add it to the working entity 
                    workingEntity[components[i]] = c;
                }
                if (foundAll)
                {
                    moveableEntitysL.Add(id);
                } else if (partial)
                {
                    staticEntitysL.Add(id);
                }
            }
            Console.WriteLine(moveableEntitysL);
            Console.WriteLine(staticEntitysL);
            moveableEntitys = moveableEntitysL.ToArray();
            staticEntitys = staticEntitysL.ToArray();
        }
    }
}
