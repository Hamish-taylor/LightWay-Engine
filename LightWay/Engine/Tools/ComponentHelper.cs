using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay.Engine.ECS.Components
{
    public class ComponentHelper
    {
        public static long bitShiftID(int i)
        {
            return (long)Math.Pow(2, i);
        }


    }
}
