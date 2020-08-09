using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay.Engine.ECS.Components
{
    public class TextC 
    {
        public string text { get; set; } = "";

        public SpriteFont font { get; set; }

        public TextC(string text, SpriteFont font)
        {
            this.text = text;
            this.font = font;
        }
    }
}
