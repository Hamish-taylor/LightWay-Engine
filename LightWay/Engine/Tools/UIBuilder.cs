using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using LightWay.Engine.ECS.Components;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LightWay.Engine.ECS.Tools;

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

        public static void test()
        {
            Console.WriteLine("Fuck");
        }


        public static void AttachButton(Texture2D texture, Vector2 pos, Vector2 scale)
        {
            if (panel == null) throw new UIException("Complete called before Begin");
            TextureC textureC = new TextureC(texture);
            entityGroup.Add(entityController.CreateEntity(textureC, new TransformC(pos,scale), new UIC(), new ButtonC(textureC,test,new Rectangle(pos.ToPoint(),new Point((int)(texture.Width * scale.X),(int)(texture.Height * scale.Y))))));
        }
        public static void AttachTexture(Texture2D texture, Vector2 pos, Vector2 scale)
        {
            if (panel == null) throw new UIException("Complete called before Begin");

            entityGroup.Add(entityController.CreateEntity(new TextureC(texture), new TransformC(pos,scale),new UIC()));
        }

        public static void AttachText(string text, string font, Vector2 pos, Vector2 scale,int wordPixelSpacing = 2, int letterPixelSpacing = 1)
        {
            if (panel == null) throw new UIException("Complete called before Begin");
            entityGroup.Add(entityController.CreateEntity(new TextureC(TextHelper.GenerateFontTexture(text, font, wordPixelSpacing, letterPixelSpacing)), new TransformC(pos,scale), new UIC()));
        }

        public static void AttachText(string text, string font,Color color, Vector2 pos, Vector2 scale, int wordPixelSpacing = 2, int letterPixelSpacing = 1)
        {
            if (panel == null) throw new UIException("Complete called before Begin");
            entityGroup.Add(entityController.CreateEntity(new TextureC(TextHelper.GenerateFontTexture(text, font,color , wordPixelSpacing,letterPixelSpacing)), new TransformC(pos,scale), new UIC()));
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
