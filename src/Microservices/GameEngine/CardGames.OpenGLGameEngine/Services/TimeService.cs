using CardGames.OpenGLGameEngine.EventArgs;

namespace CardGames.OpenGLGameEngine.Services
{
    public class TimeService
    {
        private static TimeService? _instance = null;

        private double _deltaTime;

        public double DeltaTime
        {
            get => _deltaTime;
            set
            {
                OnUpdatedTriggered();

                _deltaTime = value;
            }
        }

        public event EventHandler<TimeArgs>? UpdateTriggered;

        private TimeService()
        {

        }

        public static TimeService GetInstance()
        {
            if (_instance is null)
            {
                _instance = new TimeService();
            }

            return _instance;
        }

        private void OnUpdatedTriggered()
        {
            UpdateTriggered?.Invoke(this, new TimeArgs(DeltaTime));
        }
    }
}
