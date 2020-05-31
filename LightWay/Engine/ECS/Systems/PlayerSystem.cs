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
        public PlayerSystem()
        {
            components.Add(typeof(Position));
            components.Add(typeof(Controllable));
            components.Add(typeof(VelocityC));
            Init();
        }    
        public override void ProcessEntity()
        {
            if (Input.getKeyBoardKey(Keys.D)) ((VelocityC)workingEntity[typeof(VelocityC)]).velocity.X += 1;
            if (Input.getKeyBoardKey(Keys.A)) ((VelocityC)workingEntity[typeof(VelocityC)]).velocity.X -= 1;
            if (Input.getKeyBoardKey(Keys.S)) ((VelocityC)workingEntity[typeof(VelocityC)]).velocity.Y += 1;
            if (Input.getKeyBoardKey(Keys.Space) && ((VelocityC)workingEntity[typeof(VelocityC)]).velocity.Y == 0) ((VelocityC)workingEntity[typeof(VelocityC)]).velocity.Y -= 20;

            ((Position)workingEntity[typeof(Position)]).position += ((VelocityC)workingEntity[typeof(VelocityC)]).velocity;

            //DRAG
            //PROBABLY SHOULD SPLIT THIS INTO ITS OWN SYSTEM
            ((Position)workingEntity[typeof(Position)]).position -= ((Position)workingEntity[typeof(Position)]).position / 13;
        }

    }
}
