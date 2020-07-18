using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using LightWay.Engine.ECS.Components;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay
{
    public class UIBuilder
    {
        private static EntityController entityController;

        private static PanelC panel = null;

        private static List<Entity> components = new List<Entity>();

        private static EntityGroup entityGroup;
        /// <summary>
        /// Initilises the beginging of a UI creation. Complete must be called for the UI to be added to the entity controller
        /// </summary>
        /// <param name="entityController"></param>
        public static void Begin(EntityController entityController)
        {
            panel = null;
            components.Clear();
            entityGroup = new EntityGroup();
            UIBuilder.entityController = entityController;
            panel = new PanelC();
        }

        public static void AttachButton()
        {
            if (panel == null) throw new UIException("Complete called before Begin");

        }

        public static void AttachTexture(Texture2D texture,float x , float y, float sX, float sY)
        {
            if (panel == null) throw new UIException("Complete called before Begin");

            entityGroup.Add(entityController.CreateEntity(new TextureC(texture,new Vector2(sX,sY)), new PositionC(x, y), new UIC()));
        }
        public static void AttachTexture(Texture2D texture, Vector2 pos, Vector2 scale)
        {
            if (panel == null) throw new UIException("Complete called before Begin");

            entityGroup.Add(entityController.CreateEntity(new TextureC(texture, scale), new PositionC(pos),new UIC()));
        }

        public static void AttachText()
        {
            if (panel == null) throw new UIException("Complete called before Begin");

        }

        /// <summary>
        /// For code readability, however it serves no real purpose other then retreaving the id of the base UI panel Entity
        /// </summary>
        /// <returns> The id of the base UI Panel</returns>
        public static EntityGroup Complete()
        {
            if (panel == null) throw new UIException("Complete called before Begin");

            panel = null;
            components.Clear();
            entityGroup = null;
            return entityGroup;
        }
    }


    public class UIException : Exception {

        public UIException(string message) : base(message)
        {
        }
    }
}
