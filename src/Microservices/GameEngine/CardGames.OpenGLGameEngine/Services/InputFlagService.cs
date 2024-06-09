using CardGames.OpenGLGameEngine.Enums;

namespace CardGames.OpenGLGameEngine.Services
{
    public class InputFlagService
    {
        private static InputFlagService? _instance = null;

        public InputFlags ActiveInputFlags { get; set; }

        private InputFlagService()
        {
            ActiveInputFlags = InputFlags.Reset;
        }

        public static InputFlagService GetInstance()
        {
            if (_instance is null)
            {
                _instance = new InputFlagService();
            }

            return _instance;
        }
    }
}
