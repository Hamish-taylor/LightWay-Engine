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
        private ColliderC Collider;
        public PlayerSystem()
        {
            components.Add(typeof(PositionC));
            components.Add(typeof(ControllableC));
            components.Add(typeof(VelocityC));
            components.Add(typeof(ColliderC));
            keys = Input.keys;
            Init();
        }    
        public override void ProcessEntity()
        {
            keys = Input.keys;
            Velocity = (VelocityC)workingEntity[typeof(VelocityC)];
            Position = (PositionC)workingEntity[typeof(PositionC)];
            Collider = (ColliderC)workingEntity[typeof(ColliderC)];

            if (keys.Contains(Keys.D)) Velocity.velocity.X += 1;
            else if (keys.Contains(Keys.A)) Velocity.velocity.X -= 1;
            
            Console.WriteLine(Collider.colDir);
            if ((Velocity.velocity.X > 0 && Collider.colDir.Y != 0 || Velocity.velocity.X < 0 && Collider.colDir.W !=  0)) Velocity.velocity.X = 0;

            if (Velocity.velocity.Y > 0 && Collider.colDir.Z != 0 || Velocity.velocity.Y < 0 && Collider.colDir.X != 0) Velocity.velocity.Y = 0;
            if (keys.Contains(Keys.Space) && Velocity.velocity.Y == 0) Velocity.velocity.Y -= 20;
            Position.position += Velocity.velocity;

            //DRAG
            //PROBABLY SHOULD SPLIT THIS INTO ITS OWN SYSTEM
            Velocity.velocity -= Velocity.velocity / 8;
        }
    }
}
