using CardGames.GameEngine.Rendering;
using CardGames.GameEngine.Rendering.Buffering;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace CardGames.GameEngine
{
    public class Scene : IScene
    {
        private readonly List<IGameObject> _gameObjects;

        private readonly float[] _vertices =
        {
            -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,
             0.5f, -0.5f, -0.5f,  1.0f, 0.0f,
             0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
             0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
            -0.5f,  0.5f, -0.5f,  0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,

            -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
             0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
             0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
            -0.5f,  0.5f,  0.5f,  0.0f, 1.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,

            -0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
            -0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
            -0.5f,  0.5f,  0.5f,  1.0f, 0.0f,

             0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
             0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
             0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  1.0f, 0.0f,

            -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  1.0f, 1.0f,
             0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
             0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,

            -0.5f,  0.5f, -0.5f,  0.0f, 1.0f,
             0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
             0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
            -0.5f,  0.5f,  0.5f,  0.0f, 0.0f,
            -0.5f,  0.5f, -0.5f,  0.0f, 1.0f
        };

        public ICamera Camera { get; }

        private VertexArrayObject _vao;
        private VertexBufferObject _vbo;

        private Shader _shader;
        private Texture _texture;

        public Scene()
        {
            _gameObjects = new List<IGameObject>();
            
            Camera = new Camera();
        }

        public void AddObject(IGameObject gameObject)
        {
            _gameObjects.Add(gameObject);
        }

        public IGameObject? FindObjectById(Guid id)
        {
            return _gameObjects.FirstOrDefault(gameObject => gameObject.Id == id);
        }

        public void RemoveObject(IGameObject gameObject)
        {
            _gameObjects.Remove(gameObject);
        }

        public void Load()
        {
            GL.ClearColor(Color4.Black);

            GL.Enable(EnableCap.DepthTest);

            _vbo = new VertexBufferObject(_vertices);

            _vao = new VertexArrayObject();

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

            _texture = new Texture(GameEngineResources.AwesomeFace);
        }

        public void Render()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            _vao.Bind();

            _shader.Activate();

            _texture.Bind();

            for (var x = -10.0f; x <= 10.0f; x += 2.0f)
            {
                for (var y = -10.0f; y <= 10.0f; y += 2.0f)
                {
                    for (var z = -10.0f; z <= 10.0f; z += 2.0f)
                    {
                        var model = Matrix4.CreateTranslation(new Vector3(x, y, z));

                        _shader.SetMatrix4("model", model);

                        GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
                    }
                }
            }

            _texture.Unbind();

            _shader.SetMatrix4("view", Camera.GetViewMatrix());
            _shader.SetMatrix4("projection", Camera.GetProjectionMatrix());

            _shader.Deactivate();

            _vao.Unbind();

        }

        public void Unload()
        {
            _texture.Delete();
            _shader.Delete();
            _vao.Delete();
        }

        public void Dispose()
        {
            
        }
    }
}
