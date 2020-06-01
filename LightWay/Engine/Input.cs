using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace LightWay
{
    /// <summary>
    /// A helper class for getting input from a keyboard or controller
    /// </summary>
    class Input
    {
        /// <summary>
        /// The prefered variable for accessing pressed keys.
        /// This isnt updated every game loop for preformance.
        /// </summary>
        public static Keys[] keys { get; private set; } = new Keys[0];
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
        /// <summary>
        /// This method will refresh the keys array with the currently held down keys
        /// </summary>
        public static void UpdateKeys()
        {
            keys = Keyboard.GetState().GetPressedKeys();
        }
    }
}
