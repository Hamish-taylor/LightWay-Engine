using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LightWay.Engine.ECS.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay.Engine.ECS.Systems
{
    /// <summary>
    /// Makes makes a camera component follow the first controllable component in the CIP
    /// </summary>
    class CameraFollowSystem
    {
        GraphicsDevice graphicsDevice;
        private float cameraFollowDampening = 0.5f;

        private EntityController entityController;

        List<Entity> compatableEntitys = new List<Entity>();
        public CameraFollowSystem(GraphicsDevice graphicsDevice,EntityController entityController)
        {
            this.graphicsDevice = graphicsDevice;
            this.entityController = entityController;
        }

        public void Update(GameTime gameTime)
        {
            compatableEntitys = entityController.EntitesThatContainComponents(entityController.GetAllEntityWithComponent<TransformC>(), typeof(ControllableC));
            ProcessEntity();
        }

        public void ProcessEntity()
        {
            foreach(Entity e in compatableEntitys)
            {

                CameraC cameraC = entityController.GetAllComponent<CameraC>()[0];

                Vector3 camTranslation = cameraC.matrix.Translation;

                Vector2 playerPos = e.GetComponent<TransformC>();

                Vector3 translation = new Vector3(MathHelper.Lerp(camTranslation.X, -playerPos.X + (graphicsDevice.Viewport.Width / 2), cameraFollowDampening), MathHelper.Lerp(camTranslation.Y, -playerPos.Y + (graphicsDevice.Viewport.Height / 2), 1f), 0);

                cameraC.matrix = Matrix.CreateScale(new Vector3(1, 1, 0)) * Matrix.CreateTranslation(translation);
            }

           

          
        }       
    }
}
