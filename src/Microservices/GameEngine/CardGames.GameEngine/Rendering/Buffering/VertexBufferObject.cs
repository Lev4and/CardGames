using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace CardGames.GameEngine.Rendering.Buffering
{
    public class VertexBufferObject
    {
        private readonly int _id;

        public VertexBufferObject(IEnumerable<float> data)
        {
            _id = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, _id);

            GL.BufferData(BufferTarget.ArrayBuffer, data.Count() * sizeof(float),
                data.ToArray(), BufferUsageHint.StaticDraw);
        }

        public VertexBufferObject(IEnumerable<Vector2> data)
        {
            _id = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, _id);

            GL.BufferData(BufferTarget.ArrayBuffer, data.Count() * Vector2.SizeInBytes,
                data.ToArray(), BufferUsageHint.StaticDraw);
        }

        public VertexBufferObject(IEnumerable<Vector3> data)
        {
            _id = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, _id);

            GL.BufferData(BufferTarget.ArrayBuffer, data.Count() * Vector3.SizeInBytes,
                data.ToArray(), BufferUsageHint.StaticDraw);
        }

        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, _id);
        }

        public void Unbind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public void Delete()
        {
            GL.DeleteBuffer(_id);
        }
    }
}
