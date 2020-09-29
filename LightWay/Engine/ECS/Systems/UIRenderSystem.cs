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
        public void Update(GameTime gameTime)
        {
            compatableEntitys = entityController.EntitesThatContainComponents(entityController.GetAllEntityWithComponent<TextureC>(), typeof(TransformC), typeof(UIC));
            spriteBatch.Begin(SpriteSortMode.Deferred,null,SamplerState.PointClamp);
            ProcessEntity();
            spriteBatch.End();
        }
        public void ProcessEntity()
        {
            
            foreach (Entity e in compatableEntitys)
            {
                TextureC Texture = e.GetComponent<TextureC>();
                TransformC Pos = e.GetComponent<TransformC>();
                if(e.Active)
                spriteBatch.Draw(Texture, new Rectangle((int)Pos.Position.X, (int)Pos.Position.Y, (int)(Texture.Texture.Width*Pos.Scale.X), (int)(Texture.Texture.Height*Pos.Scale.Y)), null, Color.White, 0,Vector2.Zero, SpriteEffects.None, 0f);

            }         
        }
    }
}
