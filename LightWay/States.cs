using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace LightWay
{
    public abstract class State
    {
        public bool state = true; //default 
        public static implicit operator bool(State s) => s.state;

       // public static bool operator ==(State a, State b) => a.state && b.state;

       // public static bool operator !=(State a, State b) => !(a.state && b.state);

        public void Invert() { 
            state = !state; 
        }

    }


    public class MainMenue : State
    {


    }

    public class Dead : State
    {


    }


    public static class States
    {

        public static MainMenue MainMenue = new MainMenue();

        public static Dead Dead = new Dead();

    }
}
