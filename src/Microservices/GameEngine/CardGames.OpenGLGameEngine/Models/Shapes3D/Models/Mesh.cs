using CardGames.OpenGLGameEngine.Entities.Components;
using OpenTK.Graphics.OpenGL;

namespace CardGames.OpenGLGameEngine.Models.Shapes3D.Models
{
    public class Mesh : Shape3D
    {
        private readonly string _name;

        private readonly uint[] _indices;
        private readonly float[] _vertices;

        public string Name
        {
            get
            {
                return _name;
            }
        }

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

        public Mesh(string name, float[] vertices, uint[] indices)
        {
            _name = name;

            _indices = indices;
            _vertices = vertices;
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
