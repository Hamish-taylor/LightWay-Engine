using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay.Engine.ECS.Components
{
    /// <summary>
    /// Another bad component that shouldnt need to exist
    /// </summary>
    public class UIC : IComponent
    {
        public Type type => typeof(UIC);

    }
}
