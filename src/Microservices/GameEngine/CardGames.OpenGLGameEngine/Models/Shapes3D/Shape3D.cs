using CardGames.OpenGLGameEngine.Entities.Components;
using CardGames.OpenGLGameEngine.Scenes;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace CardGames.OpenGLGameEngine.Models.Shapes3D
{
    public abstract class Shape3D : RenderableObject
    {
        public abstract float[] Vertices { get; }

        private protected void ArrayBuffer(Shader shader)
        {
            VBO = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Length * sizeof(float), Vertices, BufferUsageHint.StaticDraw);

            VAO = GL.GenVertexArray();

            GL.BindVertexArray(VAO);

            var positionLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(positionLocation);

            GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);

            var normalLocation = shader.GetAttribLocation("aNormal");
            GL.EnableVertexAttribArray(normalLocation);

            GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 
                3 * sizeof(float));

            var texCoordLocation = shader.GetAttribLocation("aTexCoords");

            GL.EnableVertexAttribArray(texCoordLocation);

            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 
                6 * sizeof(float));

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
        }

        private protected void ElementArrayBuffer(Shader shader, uint[] indices)
        {
            VBO = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Length * sizeof(float), Vertices, BufferUsageHint.StaticDraw);

            VAO = GL.GenVertexArray();

            GL.BindVertexArray(VAO);

            EBO = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO.Value);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);


            var positionLocation = shader.GetAttribLocation("aPos");

            GL.EnableVertexAttribArray(positionLocation);

            GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);

            var normalLocation = shader.GetAttribLocation("aNormal");

            GL.EnableVertexAttribArray(normalLocation);

            GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 
                3 * sizeof(float));

            var texCoordLocation = shader.GetAttribLocation("aTexCoords");

            GL.EnableVertexAttribArray(texCoordLocation);

            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 
                6 * sizeof(float));

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

        }

        private protected void DrawShape(Shader shader, TransformComponent transform, Action glDraw)
        {
            GL.BindVertexArray(VAO);

            var camera = SceneManager.GetInstance().ActiveScene.EntityComponentManager
                .GetEntitiesWithType<CameraComponent>().FirstOrDefault()?.GetComponent<CameraComponent>() 
                    ?? throw new NullReferenceException("No Camera In Scene");

            shader.SetMatrix4("view", true, camera.GetViewMatrix());
            shader.SetMatrix4("projection", true, camera.GetProjectionMatrix());

            var translationMatrix = Matrix4.CreateTranslation(transform.Position);

            var rotationMatrix = Matrix4.CreateFromQuaternion(new Quaternion(transform.Rotation.X, transform.Rotation.Y, 
                transform.Rotation.Z, transform.Rotation.W));

            var scaleMatrix = Matrix4.CreateScale(transform.Scale);

            var modelMatrix = scaleMatrix * rotationMatrix * translationMatrix;

            shader.SetMatrix4("model", true, modelMatrix);

            glDraw.Invoke();
        }
    }
}
