using LightWay.Engine.ECS.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics.Collision;
using FarseerPhysics.Dynamics;

namespace LightWay.Engine.ECS.Systems
{   
    /// <summary>
    /// controlls the phsi
    /// </summary>
    class PhysicsSystem : System
    {
        public override void ProcessEntity()
        {
            throw new NotImplementedException();
        }


        public override void update(GameTime gameTime, ComponentIndexPool CIP)
        {
            base.update(gameTime, CIP);
        }
    }


}










/*   /// <summary>
    /// For now this system cashes all collider objects in the game
    /// This assumes that a collider will never be removed from a entity
    /// </summary>
    class PhysicsSystem : System
    {
        World world = new World(new Vector2(0,1));
        
        private int[] moveableEntitys;
        private int[] staticEntitys;
        private EntityController entityController;
        public bool start { get; set; }

        private ComponentIndexPool CIP;
        public PhysicsSystem(ComponentIndexPool CIP,EntityController entityController)
        {
            this.CIP = CIP;
            this.entityController = entityController;
            components.Add(typeof(ColliderC));
            components.Add(typeof(PositionC));
            components.Add(typeof(VelocityC));
            start = true;
            Init();
        }
        public override void update(GameTime gameTime, ComponentIndexPool CIP)
        {
            if (moveableEntitys == null || staticEntitys == null)
            {
                UpdateEntitys(CIP);
            }

            ProcessEntity();
        }
        public override void ProcessEntity()
        {
            foreach (int move in moveableEntitys)
            {
                ColliderC movable = ((ColliderC)CIP.Get(typeof(ColliderC), move));
                
                *//*foreach (int stat in staticEntitys)
                {
                    ColliderC st = ((ColliderC)CIP.Get(typeof(ColliderC), stat));
                    if (movable.collider.Intersects(st.collider))
                    {
                        Colliding(movable, st);
                    }
                }*//*
                if(entityController.currentRenderedChunk !=null)
                {
                    IComponent[] b = entityController.currentRenderedChunk.Blocks;
                    for (int i = 2; i < b.Length; i += entityController.currentRenderedChunk.numComponentsPerBlock)
                    {
                        if (b[i] != null && movable.collider.Intersects(((ColliderC)b[i]).collider))
                        {
                            Colliding(movable, (ColliderC)b[i],(PositionC)CIP.Get(typeof(PositionC),move));
                            Console.WriteLine("FUCK");
                        }
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
        public void Colliding(ColliderC one, ColliderC two, PositionC movePos)
        {
            Point centerOne = one.collider.Center;
            Point centerTwo = two.collider.Center;
            Point[] verticiesOne = one.verticies;
            Point[] verticiesTwo = two.verticies;
            
            for (int s = 0; s < 2; s++)
            {
                if(s == 1)
                {
                    Point[] temp = verticiesOne;
                    Point[] temp2 = verticiesTwo;
                    Point tempc = centerOne;
                    Point tempc2 = centerTwo;
                    verticiesOne = temp2;
                    verticiesTwo = temp;

                    centerOne = tempc2;
                    centerTwo = tempc;
                }

                for (int p = 0; p < verticiesOne.Length; p++)
                {
                    Point line1S = centerOne;
                    Point line1E = verticiesOne[p];
                    Vector2 displacement = new Vector2(0,0);

                    for (int q = 0; q < verticiesTwo.Length; q++)
                    {
                        Point line2S = verticiesTwo[q];
                        Point line2E = verticiesTwo[(q + 1) % verticiesTwo.Length];

                        float h = (line2E.X - line2S.X) * (line1S.Y - line1E.Y) - (line1S.X - line1E.X) * (line2E.Y - line2S.Y);
                        float t1 = ((line2S.Y - line2E.Y) * (line1S.X - line2S.X) + (line2E.X - line2S.X) * (line1S.Y - line2S.Y)) / h;
                        float t2 = ((line1S.Y - line1E.Y) * (line1S.X - line2S.X) + (line1E.X - line1S.X) * (line1S.Y - line2S.Y)) / h;

                        if (t1 >= 0.0f && t1 < 1.0f && t2 >= 0.0f && t2 < 1.0f)
                        {
                            displacement.X += (1.0f - t1) * (line1E.X - line1S.X);
                            displacement.Y += (1.0f - t1) * (line1E.Y - line1S.Y);
                            Console.WriteLine(displacement.X);
                        }

                    }

                    displacement.X *= (s == 0 ? -1 : +1);
                    displacement.Y *= (s == 0 ? -1 : +1);

                    movePos.position = new Vector2(movePos.position.X + displacement.X, movePos.position.Y - displacement.Y);
                    one.setPos(new Point((int)(movePos.position.X + displacement.X), (int)(movePos.position.Y - displacement.Y)));
                }
            }
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
                        }else
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
            moveableEntitys = moveableEntitysL.ToArray();
            staticEntitys = staticEntitysL.ToArray();
        }
    }*/
