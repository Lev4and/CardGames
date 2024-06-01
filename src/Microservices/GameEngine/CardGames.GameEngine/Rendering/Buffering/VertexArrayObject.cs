using OpenTK.Graphics.OpenGL;

namespace CardGames.GameEngine.Rendering.Buffering
{
    public class VertexArrayObject
    {
        private readonly int _id;

        public VertexArrayObject()
        {
            _id = GL.GenVertexArray();

            Bind();
        }

        public void Link(int location, int size, VertexBufferObject vertexBufferObject)
        {
            Bind();

            vertexBufferObject.Bind();

            GL.VertexAttribPointer(location, size, VertexAttribPointerType.Float, false, 0, 0);

            GL.EnableVertexAttribArray(location);

            Unbind();
        }

        public void Bind()
        {
            GL.BindVertexArray(_id);
        }

        public void Unbind()
        {
            GL.BindVertexArray(0);
        }

        public void Delete()
        {
            GL.DeleteVertexArray(_id);
        }
    }
}
