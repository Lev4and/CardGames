using CardGames.GameLogic;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace CardGames.GameEngine
{
    public class Scene : GameWindow
    {
        private readonly float[] _vertices =
        {    
             0.5f,  0.7f,  0.0f,  1.0f,  1.0f,
             0.5f, -0.7f,  0.0f,  1.0f,  0.0f,
            -0.5f, -0.7f,  0.0f,  0.0f,  0.0f,
            -0.5f,  0.7f,  0.0f,  0.0f,  1.0f 
        };

        private readonly uint[] _indices =
        {
            0, 1, 3,
            1, 2, 3
        };

        private VertexArrayObject _vertexArrayObject;
        private VertexBufferObject _vertexBufferObject;
        private ElementBufferObject _elementBufferObject;

        private Shader _shader;

        private Camera _camera;

        private bool _firstMove = true;

        private Vector2 _lastPosition;

        private double _time;

        private readonly ICardCollectionFactory _cardCollectionFactory;
        private readonly ICardCollection _cardCollection;

        private readonly IDictionary<string, Texture> _textures;

        public Scene(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : 
            base(gameWindowSettings, nativeWindowSettings)
        {
            _cardCollectionFactory = new CardCollectionFactory(new CardIdRange(1, 54));
            _cardCollection = _cardCollectionFactory.Create();

            _textures = new Dictionary<string, Texture>();
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(Color4.Black);

            GL.Enable(EnableCap.DepthTest);

            _vertexArrayObject = new VertexArrayObject();

            _vertexBufferObject = new VertexBufferObject(_vertices);
            _elementBufferObject = new ElementBufferObject(_indices);

            _shader = new Shader(GameEngineResources.DefaultVertexShader, GameEngineResources.DefaultFragmentShader);
            _shader.Activate();

            var vertexLocation = _shader.GetAttribProgram("aPosition");

            GL.EnableVertexAttribArray(vertexLocation);

            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, 
                false, 5 * sizeof(float), 0);

            var textureCoordLocation = _shader.GetAttribProgram("aTexCoord");

            GL.EnableVertexAttribArray(textureCoordLocation);

            GL.VertexAttribPointer(textureCoordLocation, 2, VertexAttribPointerType.Float, false, 
                5 * sizeof(float), 3 * sizeof(float));

            foreach (var card in _cardCollection)
            {
                var frontCardTextureContent = (byte[]?)GameEngineResources.ResourceManager.GetObject($"ThirdDeck{card}");

                if (frontCardTextureContent is not null)
                {
                    _textures.Add(card.ToString(), new Texture(frontCardTextureContent));
                }
            }

            var backCardTextureContent = (byte[]?)GameEngineResources.ResourceManager.GetObject($"ThirdDeckBack");

            if (backCardTextureContent is not null)
            {
                _textures.Add("Back", new Texture(backCardTextureContent));
            }

            _camera = new Camera(Vector3.UnitZ * 3, Size.X / (float)Size.Y);

            CursorState = CursorState.Grabbed;
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            _camera.Fov -= e.OffsetY;
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Key == Keys.Escape)
            {
                Close();
            }
        }

        protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
        {
            base.OnFramebufferResize(e);

            GL.Viewport(0, 0, e.Width, e.Height);

            _camera.AspectRatio = Size.X / (float)Size.Y;
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            _vertexArrayObject.Bind();

            _shader.Activate();

            for (var i = 0; i < _cardCollection.Count(); i+= 1)
            {
                _textures[_cardCollection.ElementAt(i).ToString()].Bind();

                var model = Matrix4.CreateTranslation(new Vector3(i % 8 * 1.5f, i / 8 % 8 * -1.5f, 0.0f));

                _shader.SetMatrix4("model", model);

                GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
                
                _textures[_cardCollection.ElementAt(i).ToString()].Unbind();
            }

            _shader.SetMatrix4("view", _camera.GetViewMatrix());
            _shader.SetMatrix4("projection", _camera.GetProjectionMatrix());

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            if (!IsFocused)
            {
                return;
            }

            var input = KeyboardState;

            const float cameraSpeed = 1.5f;
            const float sensitivity = 0.2f;

            if (input.IsKeyDown(Keys.W))
            {
                _camera.Position += _camera.Front * cameraSpeed * (float)args.Time;
            }

            if (input.IsKeyDown(Keys.S))
            {
                _camera.Position -= _camera.Front * cameraSpeed * (float)args.Time;
            }

            if (input.IsKeyDown(Keys.A))
            {
                _camera.Position -= _camera.Right * cameraSpeed * (float)args.Time;
            }

            if (input.IsKeyDown(Keys.D))
            {
                _camera.Position += _camera.Right * cameraSpeed * (float)args.Time;
            }

            if (input.IsKeyDown(Keys.Space))
            {
                _camera.Position += _camera.Up * cameraSpeed * (float)args.Time;
            }

            if (input.IsKeyDown(Keys.LeftShift))
            {
                _camera.Position -= _camera.Up * cameraSpeed * (float)args.Time;
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

                _camera.Yaw += deltaX * sensitivity;
                _camera.Pitch -= deltaY * sensitivity;
            }
        }

        protected override void OnUnload()
        {
            base.OnUnload();
        }
    }
}
