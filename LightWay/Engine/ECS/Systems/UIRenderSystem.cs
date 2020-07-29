using System;
using LightWay.Engine.ECS.Components;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LightWay.Engine.ECS.Systems
{
    class UIRenderSystem
    {
        private SpriteBatch spriteBatch;
        private EntityController entityController;
        float rotation = 0;
        List<Entity> compatableEntitys = new List<Entity>();
        public UIRenderSystem(SpriteBatch spriteBatch, EntityController entityController)
        {
            this.spriteBatch = spriteBatch;
            this.entityController = entityController;
        }
        public void Update(GameTime gameTime, ComponentIndexPool CIP)
        {
            compatableEntitys = entityController.EntitesThatContainComponents(entityController.GetAllEntityWithComponent<TextureC>(), typeof(TransformC), typeof(UIC));
            spriteBatch.Begin();
            ProcessEntity();
            spriteBatch.End();
        }
        public void ProcessEntity()
        {
            
            rotation += 0.01f;
            foreach (Entity e in compatableEntitys)
            {
                TextureC Texture = e.GetComponent<TextureC>();
                TransformC Pos = e.GetComponent<TransformC>();
                
                spriteBatch.Draw(Texture, new Rectangle((int)Pos.Position.X, (int)Pos.Position.Y, Texture.Texture.Width, Texture.Texture.Height), null, Color.White, rotation, new Vector2(Texture.Texture.Width / 2f, Texture.Texture.Height / 2f), SpriteEffects.None, 0f);

            }         
        }
    }
}
