using LightWay.Engine.ECS.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay
{
    class PlayerSystem : System
    {
        private Keys[] keys;
        private VelocityC Velocity;
        private PositionC Position;
        public PlayerSystem()
        {
            components.Add(typeof(PositionC));
            components.Add(typeof(ControllableC));
            components.Add(typeof(VelocityC));
            keys = Input.keys;
            Init();
        }    
        public override void ProcessEntity()
        {
            keys = Input.keys;
            Velocity = (VelocityC)workingEntity[typeof(VelocityC)];
            Position = (PositionC)workingEntity[typeof(PositionC)]; ;
         
            if (keys.Contains(Keys.D)) Velocity.velocity.X += 1;
            else if (keys.Contains(Keys.A)) Velocity.velocity.X -= 1;
            if (keys.Contains(Keys.Space) && Velocity.velocity.Y == 0) Velocity.velocity.Y -= 20;

            Position.position += Velocity.velocity;

            //DRAG
            //PROBABLY SHOULD SPLIT THIS INTO ITS OWN SYSTEM
            Position.position -= Position.position / 13;
        }

    }
}
