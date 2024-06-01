using CardGames.GameEngine.WindowSettings;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace CardGames.GameEngine
{
    public class Window : GameWindow, IWindow
    {
        private const float CameraSpeed = 1.5f;
        private const float Sensitivity = 0.2f;

        private readonly IScene _scene;

        private bool _firstMove;
        private Vector2 _lastPosition;

        private int _fps;
        private double _durationTime;

        public Window(IScene scene) :
            base(new DefaultGameWindowSettings(), new DefaultNativeWindowSettings())
        {
            _scene = scene;

            _firstMove = true;
        }

        protected override void OnLoad()
        {
            _scene.Load();

            _scene.Camera.Position = new Vector3(Vector3.UnitZ * 3);
            _scene.Camera.AspectRatio = (float)Size.X / (float)Size.Y;

            CursorState = CursorState.Grabbed;
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            _scene.Camera.Fov -= e.OffsetY;
        }

        protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
        {
            GL.Viewport(0, 0, e.Width, e.Height);

            _scene.Camera.AspectRatio = (float)Size.X / (float)Size.Y;
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            _scene.Render();

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            _durationTime += args.Time;
            _fps += 1;

            if (_durationTime > 1)
            {
                Title = $"OpenTK Window FPS: {_fps}";

                _durationTime = 0;
                _fps = 0;
            }

            if (!IsFocused)
            {
                return;
            }

            var input = KeyboardState;

            if (input.IsKeyDown(Keys.W))
            {
                _scene.Camera.Position += _scene.Camera.Front * CameraSpeed * (float)args.Time;
            }

            if (input.IsKeyDown(Keys.S))
            {
                _scene.Camera.Position -= _scene.Camera.Front * CameraSpeed * (float)args.Time;
            }

            if (input.IsKeyDown(Keys.A))
            {
                _scene.Camera.Position -= _scene.Camera.Right * CameraSpeed * (float)args.Time;
            }

            if (input.IsKeyDown(Keys.D))
            {
                _scene.Camera.Position += _scene.Camera.Right * CameraSpeed * (float)args.Time;
            }

            if (input.IsKeyDown(Keys.Space))
            {
                _scene.Camera.Position += _scene.Camera.Up * CameraSpeed * (float)args.Time;
            }

            if (input.IsKeyDown(Keys.LeftShift))
            {
                _scene.Camera.Position -= _scene.Camera.Up * CameraSpeed * (float)args.Time;
            }

            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            var mouse = MouseState;

            if (_firstMove)
            {
                _lastPosition = new Vector2(mouse.X, mouse.Y);

                _firstMove = false;
            }
            else
            {
                var deltaX = mouse.X - _lastPosition.X;
                var deltaY = mouse.Y - _lastPosition.Y;

                _lastPosition = new Vector2(mouse.X, mouse.Y);

                _scene.Camera.Yaw += deltaX * Sensitivity;
                _scene.Camera.Pitch -= deltaY * Sensitivity;
            }
        }

        protected override void OnUnload()
        {
            _scene.Unload();
        }
    }
}
