using CardGames.OpenGLGameEngine.Entities.Components;
using OpenTK.Graphics.OpenGL;

namespace CardGames.OpenGLGameEngine.Models.Shapes3D.Models
{
    public class Mesh : Shape3D
    {
        private readonly uint[] _indices;
        private readonly float[] _vertices;

        public uint[] Indices
        {
            get
            {
                return _indices;
            }
        }

        public override float[] Vertices 
        {
            get
            {
                return _vertices;
            }
        }

        public Mesh(float[] vertices, uint[] indices)
        {
            _vertices = vertices;
            _indices = indices;
        }

        public override void BindAndBuffer(Shader shader)
        {
            ElementArrayBuffer(shader, _indices);
        }

        public override void Draw(Shader shader, TransformComponent transform)
        {
            DrawShape(shader, transform, () => GL.DrawElements(PrimitiveType.Triangles, _indices.Count(), 
                DrawElementsType.UnsignedInt, 0));
        }
    }
}
