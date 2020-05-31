using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace LightWay
{
    class RenderSystem : System
    {
        public SpriteBatch spriteBatch { get; private set; }

        public RenderSystem(SpriteBatch spriteBatch)
        {
            components.Add(typeof(PositionC));
            components.Add(typeof(TextureC));
            this.spriteBatch = spriteBatch;
            Init();
        }
        public override void update(GameTime gameTime, ComponentIndexPool CIP)
        {
            spriteBatch.Begin();
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
            spriteBatch.End();
        }
        public override void ProcessEntity()
        {
            spriteBatch.Draw(((TextureC)workingEntity[typeof(TextureC)]).texture, ((PositionC)workingEntity[typeof(PositionC)]).position,Color.White);
        }


    }

}
