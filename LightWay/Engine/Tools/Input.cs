using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;

namespace LightWay.Engine.ECS.Tools
{
    /// <summary>
    /// A helper class for getting input from a keyboard or controller
    /// </summary>
    public class Input
    {
        public delegate void OnClickEventHandler(MouseClickEventArgs e);

        private static MouseState prevMouseState;

        public static event OnClickEventHandler OnClickEvent;

        public static event OnClickEventHandler OnReleaseEvent;

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

        public static void CheckMouse()
        {
            MouseState mouseState = Mouse.GetState();
            if((mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released) || (mouseState.RightButton == ButtonState.Pressed && prevMouseState.RightButton == ButtonState.Released)) {
                OnClick(new MouseClickEventArgs(mouseState));
            }

            if ((mouseState.LeftButton == ButtonState.Released && prevMouseState.LeftButton == ButtonState.Pressed) || (mouseState.RightButton == ButtonState.Released && prevMouseState.RightButton == ButtonState.Pressed))
            {               
                OnRelease(new MouseClickEventArgs(mouseState));
            }
            prevMouseState = mouseState;
        }

        public static void OnClick(MouseClickEventArgs e)
        {
            OnClickEvent?.Invoke(e);
        }

        public static void OnRelease(MouseClickEventArgs e)
        {
            OnReleaseEvent?.Invoke(e);
        }
    }
}
