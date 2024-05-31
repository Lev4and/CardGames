using CardGames.GameEngine.Scenes;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

var gameWindowSettings = new GameWindowSettings();

var nativeWindowSettings = new NativeWindowSettings()
{
    Profile = ContextProfile.Core,
    APIVersion = new Version(3, 3),
    ClientSize = new Vector2i(1280, 720),
    Flags = ContextFlags.ForwardCompatible,
    MinimumClientSize = new Vector2i(1280, 720),
};

using (var scene = new DeckScene(gameWindowSettings, nativeWindowSettings))
{
    scene.Run();
}