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
    class UIRenderSystem : System
    {
        private SpriteBatch spriteBatch;
        public UIRenderSystem(SpriteBatch spriteBatch)
        {
            components.Add(typeof(TextureC));
            components.Add(typeof(PositionC));
            components.Add(typeof(UIC));
            this.spriteBatch = spriteBatch;
        }
        public override void update(GameTime gameTime, ComponentIndexPool CIP)
        {
            spriteBatch.Begin();
            base.update(gameTime, CIP);
            spriteBatch.End();
        }
        public override void ProcessEntity()
        {
            
            TextureC Texture = (TextureC)workingEntity[typeof(TextureC)];
            PositionC Pos = (PositionC)workingEntity[typeof(PositionC)];        
            spriteBatch.Draw(Texture.Texture, Pos.position);
        }
    }
}
