using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace LightWay
{
    class Input
    {

        /// <summary>
        ///Returns the current gamePadKey
        /// </summary>
        public static ButtonState getGamePadKey()
        {
            return ButtonState.Pressed;
        }
        /// <summary>
        ///Returns all currently pressed keys on the keyboard
        /// </summary>
        public static Keys[] getKeyBoardKeys()
        {
            return Keyboard.GetState().GetPressedKeys();
        }

        /// <summary>
        ///Returns true if the "key" is being pressed on the keyboard
        /// </summary>
        public static bool getKeyBoardKey(Keys key)
        {
            return Keyboard.GetState().GetPressedKeys().Contains(key);
        }




    }
}
