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
    /// Makes makes a camera component follow the first controllable component in the CIP
    /// </summary>
    class CameraFollowSystem : System
    {
        GraphicsDevice graphicsDevice;
        private float cameraFollowDampening = 0.5f;
        public CameraFollowSystem(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            components.Add(typeof(PositionC));
            components.Add(typeof(ControllableC));
        }

        public override void update(GameTime gameTime, ComponentIndexPool CIP)
        {
            base.update(gameTime, CIP);
            ProcessEntity(CIP);
        }

        public void ProcessEntity(ComponentIndexPool CIP)
        {
            Vector3 camTranslation = ((CameraC)(CIP.GetAll(typeof(CameraC)).First().Value)).matrix.Translation;
            Vector2 playerPos = ((PositionC)workingEntity[typeof(PositionC)]).position;
            Vector3 translation = new Vector3(MathHelper.Lerp(camTranslation.X, -playerPos.X + (graphicsDevice.Viewport.Width / 2), cameraFollowDampening), MathHelper.Lerp(camTranslation.Y, -playerPos.Y + (graphicsDevice.Viewport.Height / 2), 0.5f),0);         
            
           ((CameraC)(CIP.GetAll(typeof(CameraC)).First().Value)).matrix = Matrix.CreateScale(new Vector3(1, 1, 0)) * Matrix.CreateTranslation(translation);
        }

        public override void ProcessEntity()
        {
           //This is not needed
        }

        
    }
}
