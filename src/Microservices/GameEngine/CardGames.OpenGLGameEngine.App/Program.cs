using CardGames.OpenGLGameEngine.Engine;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

var gameWindowSettings = GameWindowSettings.Default;

var nativeWindowSettings = new NativeWindowSettings()
{
    NumberOfSamples = 4,
    Profile = ContextProfile.Core,
    APIVersion = new Version(3, 3),
    ClientSize = new Vector2i(1280, 720),
    Flags = ContextFlags.ForwardCompatible,
    MinimumClientSize = new Vector2i(1280, 720),
};

using (var window = new Window(gameWindowSettings, nativeWindowSettings, false))
{
    window.Run();
}
