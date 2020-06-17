using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay
{
    class CameraC : IComponent
    {
        public Type type{ get; private set; } = typeof(CameraC);

        public Matrix matrix = new Matrix();
    }
}