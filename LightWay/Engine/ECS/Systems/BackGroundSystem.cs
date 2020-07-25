using LightWay.Engine.ECS.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay.Engine.ECS.Systems
{
    class BackGroundSystem
    {
        SpriteBatch spriteBatch;
        CameraC camera;
        GraphicsDevice graphicsDevice;
        EntityController entityController;

        List<Entity> compatableEntitys = new List<Entity>();
        public BackGroundSystem(GraphicsDevice graphicsDevice,EntityController entityController)
        {
            this.graphicsDevice = graphicsDevice;
            this.entityController = entityController;
            spriteBatch = new SpriteBatch(graphicsDevice);
        }

        public void Update(GameTime gameTime, ComponentIndexPool CIP)
        {
            compatableEntitys = entityController.EntitesThatContainComponents(entityController.GetAllEntityWithComponent<TextureC>(), typeof(PositionC), typeof(BackGroundC));
            camera = ((CameraC)CIP.GetAll(typeof(CameraC)).First().Value);
            spriteBatch.Begin(SpriteSortMode.Texture, null, SamplerState.PointWrap, null, null, null, camera.matrix);
            ProcessEntity();
            spriteBatch.End();
        }
        public void ProcessEntity()
        {
            foreach(Entity e in compatableEntitys)
            {
                TextureC Texture = e.GetComponent<TextureC>();
                PositionC Pos = e.GetComponent<PositionC>();
                BackGroundC backGround = e.GetComponent<BackGroundC>();

                Vector3 position = new Vector3(Pos.position.X + (camera.matrix.Translation.X * backGround.moveRatio), Pos.position.Y, 0);

                float width = Texture.Texture.Width * Texture.scale.X;
                float height = Texture.Texture.Height * Texture.scale.Y;
                if (graphicsDevice.Viewport.Bounds.Contains(Pos.position.X + (camera.matrix.Translation.X * -backGround.moveRatio) + width + 50, 0) && !backGround.leftNeighbour)
                {
                    entityController.CreateEntityDelayed(new PositionC(new Vector2(Pos.position.X + width, Pos.position.Y)), Texture, new BackGroundC(backGround.moveRatio));
                    backGround.leftNeighbour = true;
                }

                spriteBatch.Draw(Texture.Texture, new Rectangle((int)position.X, (int)position.Y, (int)width, (int)height), Color.White);
            }

        }
    }
}
