using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay
{
    class EntityController
    {
        public int entityCount { get; private set; } = 0;
        private GraphicsDevice graphicsDevice { get; set; }
        public EntityController(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            Init();
        }
        public List<ISystem> systems { get; set; } = new List<ISystem>();

        public List<IEntity> entitys { get; set; } = new List<IEntity>();
         
        public ComponentIndexPool CIP { get; private set; }
        public void Update(GameTime gameTime)
        {
            foreach (var s in systems)
            {
                s.update(gameTime,CIP);
            }
        }
        public void Init()
        {
            InitCIP();
            InitSystems();
        }
        private void InitSystems()
        {
            systems.Add(new RenderSystem(new SpriteBatch(graphicsDevice)));
        }
        private void InitCIP()
        {
            this.CIP = new ComponentIndexPool();
        }

        public void CreateEntity(params IComponent[] components)
        {
            Entity entity = new Entity(entityCount);
            foreach (IComponent item in components)
            {
                CIP.InsertComponent(item,entityCount);
                entity.components.Add(item);
            }
            entityCount++;
        }
    }
}
