using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MapEditor.Application
{
    class ExtendedMouse
    {
        private static MouseState previousMouseState;
        private static MouseState currentMouseState;

        public static void Update()
        {
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
        }
        public static Point GetMousePosition()
        {
            return new Point(currentMouseState.X, currentMouseState.Y);
        }
        public static Point GetPreviousMousePosition()
        {
            return new Point(currentMouseState.X, currentMouseState.Y);
        }
        public static Point GetMouseMovement()
        {
            return new Point(currentMouseState.X - previousMouseState.X, currentMouseState.Y - previousMouseState.Y);
        }
        public static int GetWheelMovement()
        {
            return currentMouseState.ScrollWheelValue - previousMouseState.ScrollWheelValue;
        }
        public static bool LeftClickDown()
        {
            return currentMouseState.LeftButton == ButtonState.Pressed;
        }
        public static bool RightClickDown()
        {
            return currentMouseState.RightButton == ButtonState.Pressed;
        }
        public static bool MiddleClickDown()
        {
            return currentMouseState.MiddleButton == ButtonState.Pressed;
        }
        public static bool PreviousLeftClickDown()
        {
            return previousMouseState.LeftButton == ButtonState.Pressed;
        }
        public static bool PreviousRightClickDown()
        {
            return previousMouseState.RightButton == ButtonState.Pressed;
        }
        public static bool PreviousMiddleClickDown()
        {
            return previousMouseState.MiddleButton == ButtonState.Pressed;
        }
        public static bool LeftClickDownAfterUp()
        {
            return currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released;
        }
        public static bool RightClickDownAfterUp()
        {
            return currentMouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton == ButtonState.Released;
        }
        public static bool MiddleClickDownAfterUp()
        {
            return currentMouseState.MiddleButton == ButtonState.Pressed && previousMouseState.MiddleButton == ButtonState.Released;
        }
    }
}
