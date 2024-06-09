using BepuPhysics;
using BepuUtilities;
using CardGames.OpenGLGameEngine.Attributes;
using CardGames.OpenGLGameEngine.Models.Physics;
using CardGames.OpenGLGameEngine.Scenes;
using CardGames.OpenGLGameEngine.Services;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using static CardGames.OpenGLGameEngine.Models.Physics.NarrowPhaseCallbacks;

namespace CardGames.OpenGLGameEngine.Engine
{
    public class Window : GameWindow
    {
        private readonly bool _isFullScreenLaunch;

        private int _frameCount;
        private bool _isFirstMove;
        private Vector2 _lastPosition;

        private readonly SceneManager _sceneManager;

        private readonly TimeService _timeService;
        private readonly WindowService _windowService;
        private readonly PhysicsService _physicsService;

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings, bool isFullScreenLaunch) : 
            base(gameWindowSettings, nativeWindowSettings)
        {
            _isFullScreenLaunch = isFullScreenLaunch;

            _frameCount = 0;
            _isFirstMove = true;
            _lastPosition = Vector2.Zero;

            _sceneManager = SceneManager.GetInstance();

            _timeService = TimeService.GetInstance();

            _windowService = WindowService.GetInstance();
            _windowService.GameWindowReference = this;

            _physicsService = PhysicsService.GetInstance();
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            _windowService.WindowState = _isFullScreenLaunch ? WindowState.Fullscreen : WindowState.Normal;

            _physicsService.BufferPool = new BepuUtilities.Memory.BufferPool();
            _physicsService.CollidableMaterials = new CollidableProperty<SimpleMaterial>();
            _physicsService.Simulation = Simulation.Create(_physicsService.BufferPool, 
                new NarrowPhaseCallbacks() { CollidableMaterials = _physicsService.CollidableMaterials }, 
                    new PoseIntegratorCallbacks(new System.Numerics.Vector3(0.0f, -10.0f, 0.0f)), 
                        new SolveDescription(8, 1));

            var targetThreadCount = int.Max(1, 
                Environment.ProcessorCount > 4 
                    ? Environment.ProcessorCount - 2 
                    : Environment.ProcessorCount - 1);

            _physicsService.ThreadDispatcher = new ThreadDispatcher(targetThreadCount);

            _sceneManager.AddScene(new BaseDebugScene("Base"));

            _sceneManager.SwapScene(0);
            _sceneManager.LoadScene(0);

            _windowService.ActiveCursorState = CursorState.Grabbed;
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);

            _sceneManager.DrawActiveScene();

            SwapBuffers();

            _sceneManager.RefreshActiveScene();

            _frameCount++;

            _sceneManager.SetActiveComponentReferences(new OnTickAttribute(), _frameCount);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            _sceneManager.UpdateActiveScene();

            if (!IsFocused)
            {
                return;
            }

            _sceneManager.UpdateActiveInput(args, KeyboardState, MouseState, ref _isFirstMove, ref _lastPosition);

            _timeService.DeltaTime = args.Time;

            _physicsService.Simulation.Timestep((float)args.Time, _physicsService.ThreadDispatcher);
        }

        protected override void OnTextInput(TextInputEventArgs e)
        {
            base.OnTextInput(e);

            _sceneManager.SetActiveComponentReferences(new CharacterPressAttribute(), (char)e.Unicode);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Size.X, Size.Y);

            HandleResizeAttributes(Size);
        }

        private void HandleResizeAttributes(Vector2 screenSize)
        {
            _sceneManager.SetActiveComponentReferences(new OnResizeAttribute(), screenSize);
        }
    }
}
