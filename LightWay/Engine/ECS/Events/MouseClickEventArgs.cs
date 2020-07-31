using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay.Engine
{
    public class MouseClickEventArgs : EventArgs
    {
        public MouseState MouseState { get; set; }
        public MouseClickEventArgs(MouseState mouseState)
        {
            mouseState = MouseState;
        }
    }
}
