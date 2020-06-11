using LightWay.Engine.ECS.Systems;
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
    /// A manager class the handles creating Entitys and executing Systems
    /// </summary>
    class EntityController
    {
        public int entityCount { get; private set; } = 0;
        private GraphicsDevice graphicsDevice { get; set; }
        public EntityController(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            InitCIP();
            InitSystems();
        }
        //For non rendering systems
        public List<ISystem> generalSystems { get; set; } = new List<ISystem>();
        public List<ISystem> renderingSystems { get; set; } = new List<ISystem>();

         
        public List<IEntity> entitys { get; set; } = new List<IEntity>();
         
        public ComponentIndexPool CIP { get; private set; }

        /// <summary>
        /// Updates any non rendering based Systems
        /// </summary>
        /// <param name="gameTime">The games <c>GameTime</c></param>
        public void GeneralUpdate(GameTime gameTime)
        {
            foreach (var s in generalSystems)
            {
                s.update(gameTime,CIP);
            }
        }
        /// <summary>
        /// Updates rendering based systems
        /// </summary>
        /// <param name="gameTime">The games <c>GameTime</c></param>
        public void RenderingUpdate(GameTime gameTime)
        {
            foreach (var s in renderingSystems)
            {
                s.update(gameTime, CIP);
            }
        }

        /// <summary>
        /// Where systems are added to the EntiyController.
        /// Currently you have to add them here manually 
        /// </summary>
        private void InitSystems()
        {
            generalSystems.Add(new CollisionSystem(CIP));
            generalSystems.Add(new GravitySystem());
            generalSystems.Add(new PlayerSystem());
           
            renderingSystems.Add(new RenderSystem(new SpriteBatch(graphicsDevice)));
        }
        /// <summary>
        /// Creates a ComponentIndexPool.
        /// Every EntityController needs a CIP.
        /// </summary>
        private void InitCIP()
        {
            this.CIP = new ComponentIndexPool();
        }
        /// <summary>
        /// Creates an entity out of passed in Components
        /// </summary>
        /// <param name="components">Your entitys components</param>
        public void CreateEntity(params IComponent[] components)
        {
            Entity entity = new Entity(entityCount);
            foreach (IComponent item in components)
            {
                CIP.InsertComponent(item,entityCount);
                entity.components.Add(item);
            }
            entitys.Add(entity);
            entityCount++;
        }
    }
}
