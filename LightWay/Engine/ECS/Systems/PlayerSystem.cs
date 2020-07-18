using LightWay.Engine.ECS.Components;
using LightWay.Engine.ECS.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay.Engine.ECS.Systems
{
    class PlayerSystem : System
    {
        private Keys[] keys;
        private PositionC Position;
        public PlayerSystem()
        {
            components.Add(typeof(PositionC));
            components.Add(typeof(ControllableC));
            keys = Input.keys;
            Init();
        }    
        public override void ProcessEntity()
        {
            keys = Input.keys;
            Position = (PositionC)workingEntity[typeof(PositionC)];
            Vector2 velocity = new Vector2();
            if (keys.Contains(Keys.D)) velocity.X += 1f;
            if (keys.Contains(Keys.A)) velocity.X -= 1f;
            if (keys.Contains(Keys.W) && Position.body.LinearVelocity.Y == 0) velocity.Y -= 20f;
            if (keys.Contains(Keys.S)) velocity.Y += 1f;

       
            Position.addForce(velocity);
            //Console.WriteLine(Position.position.ToString());
            //DRAG
            //PROBABLY SHOULD SPLIT THIS INTO ITS OWN SYSTEM
         
        }
    }
}
