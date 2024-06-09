using CardGames.OpenGLGameEngine.Entities.Components;
using CardGames.OpenGLGameEngine.Scenes;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace CardGames.OpenGLGameEngine.Models.Skybox
{
    public class Skybox : RenderableObject
    {
        private float[] _skyboxVertices =
            [
                -1.0f,  1.0f, -1.0f,
                -1.0f, -1.0f, -1.0f,
                 1.0f, -1.0f, -1.0f,
                 1.0f, -1.0f, -1.0f,
                 1.0f,  1.0f, -1.0f,
                -1.0f,  1.0f, -1.0f,

                -1.0f, -1.0f,  1.0f,
                -1.0f, -1.0f, -1.0f,
                -1.0f,  1.0f, -1.0f,
                -1.0f,  1.0f, -1.0f,
                -1.0f,  1.0f,  1.0f,
                -1.0f, -1.0f,  1.0f,

                 1.0f, -1.0f, -1.0f,
                 1.0f, -1.0f,  1.0f,
                 1.0f,  1.0f,  1.0f,
                 1.0f,  1.0f,  1.0f,
                 1.0f,  1.0f, -1.0f,
                 1.0f, -1.0f, -1.0f,

                -1.0f, -1.0f,  1.0f,
                -1.0f,  1.0f,  1.0f,
                 1.0f,  1.0f,  1.0f,
                 1.0f,  1.0f,  1.0f,
                 1.0f, -1.0f,  1.0f,
                -1.0f, -1.0f,  1.0f,

                -1.0f,  1.0f, -1.0f,
                 1.0f,  1.0f, -1.0f,
                 1.0f,  1.0f,  1.0f,
                 1.0f,  1.0f,  1.0f,
                -1.0f,  1.0f,  1.0f,
                -1.0f,  1.0f, -1.0f,

                -1.0f, -1.0f, -1.0f,
                -1.0f, -1.0f,  1.0f,
                 1.0f, -1.0f, -1.0f,
                 1.0f, -1.0f, -1.0f,
                -1.0f, -1.0f,  1.0f,
                 1.0f, -1.0f,  1.0f
            ];

        private Texture _cubeMapTexture = null!;

        private IEnumerable<string> _faces;

        public Skybox(IEnumerable<string> faces)
        {
            _faces = faces;
        }

        public override void BindAndBuffer(Shader shader)
        {
            VBO = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);

            GL.BufferData(BufferTarget.ArrayBuffer, _skyboxVertices.Length * sizeof(float), 
                _skyboxVertices, BufferUsageHint.StaticDraw);

            VAO = GL.GenVertexArray();

            GL.BindVertexArray(VAO);

            var positionLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(positionLocation);

            GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 
                3 * sizeof(float), 0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            GL.BindVertexArray(0);

            _cubeMapTexture = new Texture(_faces);
        }

        public override void Draw(Shader shader, TransformComponent transform)
        {
            GL.DepthMask(false);
            GL.DepthFunc(DepthFunction.Lequal);

            shader.Activate();

            GL.BindVertexArray(VAO);

            _cubeMapTexture.Bind(TextureUnit.Texture0);
            _cubeMapTexture.Bind(TextureTarget.TextureCubeMap);

            var camera = SceneManager.GetInstance().ActiveScene.EntityComponentManager
                .GetEntitiesWithType<CameraComponent>().FirstOrDefault()?.GetComponent<CameraComponent>() 
                    ?? throw new NullReferenceException("No Camera In Scene");

            var matWithoutFinal = new Matrix4(new Matrix3(camera.GetViewMatrix()));

            shader.SetMatrix4("view", false, matWithoutFinal);
            shader.SetMatrix4("projection", false, camera.GetProjectionMatrix());

            GL.DrawArrays(PrimitiveType.Triangles, 0, 36);

            GL.DepthMask(true);

            _cubeMapTexture.Unbind();

            shader.Deactivate();

            GL.BindVertexArray(0);

            GL.DepthFunc(DepthFunction.Less);
        }
    }
}
