using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;

namespace CardGames.GameEngine.WindowSettings
{
    public class DefaultNativeWindowSettings : NativeWindowSettings
    {
        public DefaultNativeWindowSettings()
        {
            APIVersion = new Version(3, 3);
            ClientSize = new Vector2i(640, 360);
            MinimumClientSize = new Vector2i(640, 360);
        }
    }
}
