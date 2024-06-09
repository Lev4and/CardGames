using OpenTK.Windowing.GraphicsLibraryFramework;

namespace CardGames.OpenGLGameEngine.EventArgs
{
    public class ComponentEventArgs : System.EventArgs
    {
        public KeyboardState KeyboardState { get; private set; }
        
        public ComponentEventArgs(KeyboardState keyboardState)
        {
            KeyboardState = keyboardState;
        }
    }
}
