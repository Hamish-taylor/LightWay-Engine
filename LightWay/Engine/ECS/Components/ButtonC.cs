using LightWay.Engine.ECS.Tools;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay.Engine.ECS.Components
{
    class ButtonC : IComponent
    {
        public Type type => typeof(ButtonC);

        public delegate void doWhenClick();
        private doWhenClick method;
        public TextureC textureComponent { get; private set; }

        public ButtonC(TextureC textureComponent, doWhenClick doWhenClick)
        {
            this.textureComponent = textureComponent;
            Input.OnClickEvent += OnClick;
            Input.OnReleaseEvent += OnRelease;
            method = doWhenClick;
        }

        private void OnRelease(MouseClickEventArgs e)
        {
            Console.WriteLine("Release! @" + e.MouseState);
        }

        public void OnClick(MouseClickEventArgs e)
        {
            Console.WriteLine("Click! @" + e.MouseState);
            method.Invoke();
        }

    }
}
