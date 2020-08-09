using LightWay.Engine.ECS.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LightWay.Engine.ECS.Systems
{
    class RenderSystem
    {
        public SpriteBatch spriteBatch { get; private set; }

        private TextureC TextureC;
        private Texture2D Texture;
        private TransformC Transform;
        GraphicsDevice graphicsDevice;
        EntityController entityController;

        List<Entity> compatableEntitys = new List<Entity>();
        public RenderSystem(SpriteBatch spriteBatch,GraphicsDevice graphicsDevice, EntityController entityController)
        {
            this.graphicsDevice = graphicsDevice;
            this.entityController = entityController;
            this.spriteBatch = spriteBatch;
        }
        public void Update(GameTime gameTime)
        {
            CameraC camera = entityController.GetAllComponent<CameraC>()[0];
            compatableEntitys = entityController.EntitesThatContainComponents(entityController.GetAllEntityWithComponent<TransformC>(), typeof(TextureC), typeof(ForgroundC));
            spriteBatch.Begin(SpriteSortMode.Texture,null,null,null,null,null,camera.matrix);
            ProcessEntity();
            spriteBatch.End();
        }
        public void ProcessEntity()
        {
            foreach (Entity e in compatableEntitys)
            {
                TextureC = e.GetComponent<TextureC>();
                Texture = TextureC.Texture;
                Transform = e.GetComponent<TransformC>();

                float width = Texture.Width * Transform.Scale.X;
                float height = Texture.Height * Transform.Scale.Y;
                spriteBatch.Draw(Texture, new Rectangle((int)Transform.Position.X, (int)Transform.Position.Y, (int)width, (int)height), Color.White);
            }
            
        }
    }

}
