using FarseerPhysics.Collision;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using LightWay.Engine.ECS.Components;
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
    public class EntityController
    {
        public int entityCount { get; private set; } = 0;
        private GraphicsDevice graphicsDevice { get; set; }

        public ChunkC currentRenderedChunks = null;

        public World physicsWorld = new World(new Vector2(0,5f));

        public List<Entity> stagedEntitys = new List<Entity>();

        public EntityController(GraphicsDevice graphicsDevice)
        {
            // physicsWorld.
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
            physicsWorld.Step(0.0166f);
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
            InsertStagedEntitys();
            foreach (var s in renderingSystems)
            {
                s.update(gameTime, CIP);
            }
        }

        private void InsertStagedEntitys()
        {
            foreach(var entity in stagedEntitys)
            {           
                foreach (IComponent component in entity.components)
                {
                    CIP.InsertComponent(component, entity.id);
                }
                entitys.Add(entity);
            }
            stagedEntitys.Clear();
        }

        /// <summary>
        /// Where systems are added to the EntiyController.
        /// Currently you have to add them here manually 
        /// </summary>
        private void InitSystems()
        {
            //generalSystems.Add(new PhysicsSystem(CIP,this));
            renderingSystems.Add(new BackGroundSystem(graphicsDevice,this));
            renderingSystems.Add(new ChunkSystem(graphicsDevice, this,CIP, 100));
            generalSystems.Add(new PlayerSystem());
            generalSystems.Add(new CameraFollowSystem(graphicsDevice));
            renderingSystems.Add(new RenderSystem(new SpriteBatch(graphicsDevice), graphicsDevice));

            renderingSystems.Add(new UIRenderSystem(new SpriteBatch(graphicsDevice)));
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
        public Entity CreateEntity(params IComponent[] components)
        {
            Entity entity = new Entity(entityCount);
            foreach (IComponent component in components)
            {
                CIP.InsertComponent(component, entityCount);

                entity.components.Add(component);
            }
            entitys.Add(entity);
            entityCount++;
            return entity;
        }
        /// <summary>
        /// Creates an entity out of passed in Components
        /// </summary>
        /// <param name="components">Your entitys components</param>
        public int CreateEntityDelayed(params IComponent[] components)
        {
            Entity entity = new Entity(entityCount);
            foreach (IComponent component in components)
            {           
                entity.components.Add(component);
            }
            entitys.Add(entity);
            entityCount++;
            stagedEntitys.Add(entity);
            return entity.id;           
        }
        public int GetFreeEntityId()
        {
            return ++entityCount;
        }
    }
}
