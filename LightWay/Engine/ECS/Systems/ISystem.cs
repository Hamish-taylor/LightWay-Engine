using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay
{
    interface ISystem
    {
        void update(GameTime gameTime, ComponentIndexPool CIP);
    }
}
