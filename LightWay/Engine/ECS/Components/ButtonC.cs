using LightWay.Engine.ECS.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay.Engine.ECS.Components
{
    class ButtonC
    {

        public delegate void doWhenClick();
        private Rectangle bounds;
        private doWhenClick method = null;
        private State state = null;

        public TextureC textureComponent { get; private set; }

        public ButtonC(TextureC textureComponent, doWhenClick doWhenClick, Rectangle bounds)
        {
            this.bounds = bounds;
            this.textureComponent = textureComponent;
            Input.OnClickEvent += OnClick;
            Input.OnReleaseEvent += OnRelease;
            method = doWhenClick;
        }

        public ButtonC(TextureC textureComponent, State state, Rectangle bounds)
        {
            this.state = state;
            this.bounds = bounds;
            this.textureComponent = textureComponent;
            Input.OnClickEvent += OnClick;
            Input.OnReleaseEvent += OnRelease;
        }

        public ButtonC(TextureC textureComponent,doWhenClick doWhenClick, State state, Rectangle bounds)
        {
            this.state = state;
            this.bounds = bounds;
            this.textureComponent = textureComponent;
            Input.OnClickEvent += OnClick;
            Input.OnReleaseEvent += OnRelease;
            method = doWhenClick;
        }

        private void OnRelease(MouseClickEventArgs e)
        {
            if (bounds.Contains(Mouse.GetState().Position))
            {
                if(state != null) state.Invert();
                Console.WriteLine("Release! @" + e.MouseState);

                
            }
            
        }

        public void OnClick(MouseClickEventArgs e)
        {
            if(bounds.Contains(Mouse.GetState().Position))
            {
                Console.WriteLine("Click! @" + e.MouseState);
                method.Invoke();
            }            
        }

    }
}
