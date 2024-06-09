using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace CardGames.OpenGLGameEngine.Services
{
    public class WindowService
    {
        private static WindowService? _instance = null;

        public GameWindow GameWindowReference { get; set; } = null!;

        public CursorState ActiveCursorState
        {
            get => GameWindowReference.CursorState;
            set => GameWindowReference.CursorState = value;
        }

        public WindowState WindowState
        {
            get => GameWindowReference.WindowState;
            set => GameWindowReference.WindowState = value;
        }

        public Vector2i ScreenSize
        {
            get => GameWindowReference.Size;
            set => GameWindowReference.Size = value;
        }

        private WindowService()
        {
            
        }

        public static WindowService GetInstance()
        {
            if (_instance is null)
            {
                _instance = new WindowService();
            }

            return _instance;
        }
    }
}
