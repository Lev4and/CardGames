namespace CardGames.OpenGLGameEngine.EventArgs
{
    public class TimeArgs : System.EventArgs
    {
        public double DeltaTime { get; private set; }
        
        public TimeArgs(double deltaTime)
        {
            DeltaTime = deltaTime;
        }
    }
}
