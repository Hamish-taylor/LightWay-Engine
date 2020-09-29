using FarseerPhysics.Dynamics;
using LightWay.Engine.ECS.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay.Engine.Tools
{
    class Imui
    {
        private static SpriteBatch spriteBatch;

        public static void Init()
        {
            spriteBatch = new SpriteBatch(Game1.graphicsDevice);
        }

        public static void Begin()
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointWrap);
        }
        
        
        public static void Text(int x,int y,string text,int scaleX = 1,int scaleY = 1)
        {           
            Texture2D t = TextHelper.GenerateFontTexture(text, "default");
            spriteBatch.Draw(t,new Rectangle(x,y,t.Width * scaleX,t.Height*scaleY),Color.White);
        }

        public static bool Button(int x, int y, string text = "", int scaleX = 1, int scaleY = 1, int paddingX = 0, int paddingY = 0)
        {
            
            Texture2D txt = TextHelper.GenerateFontTexture(text, "default");
            Texture2D back = TextureBuilder.Rectangle(txt.Width + paddingX, txt.Height + paddingY*2, Color.White);

            Rectangle backRec = new Rectangle(x, y, back.Width * scaleX, back.Height * scaleY);
            

            spriteBatch.Draw(back, backRec , Color.White);
            spriteBatch.Draw(txt, new Rectangle(x+(paddingX*scaleX), y+(paddingY*scaleY), txt.Width * scaleX, txt.Height * scaleY), Color.White);

            if (backRec.Contains(new Point(Input.prevMouseState.X, Input.prevMouseState.Y)) && Input.prevMouseState.LeftButton == ButtonState.Pressed) return true;
            return false;
        }


        public static void End()
        {
            spriteBatch.End();          
        }
    }
}
