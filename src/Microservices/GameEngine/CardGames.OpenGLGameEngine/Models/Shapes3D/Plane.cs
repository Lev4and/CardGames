using CardGames.OpenGLGameEngine.Entities.Components;
using CardGames.OpenGLGameEngine.Services;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace CardGames.OpenGLGameEngine.Models.Shapes3D
{
    public class Plane : Shape3D
    {
        private uint _dimensions;

        public override float[] Vertices
        {
            get
            {
                return CreatePlaneVertices();
            }
        }

        public uint[] Indices
        {
            get
            {
                return CreatePlaneIndices();
            }
        }

        public Plane(uint dimensions)
        {
            _dimensions = dimensions < 2 ? 2 : dimensions;
        }

        public override void BindAndBuffer(Shader shader)
        {
            ElementArrayBuffer(shader, Indices);
        }

        public override void Draw(Shader shader, TransformComponent transform)
        {
            DrawShape(shader, transform, () => GL.DrawElements(PrimitiveType.Triangles, 
                Indices.Length, DrawElementsType.UnsignedInt, 0));
        }

        private float[] CreatePlaneVertices()
        {
            var normals = new List<Vector3>();
            var textures = new List<Vector2>();
            var positions = new List<Vector3>();

            var halfDim = _dimensions / 2;

            for (var i = 0; i < _dimensions; i++)
            {
                for (var j = 0; j < _dimensions; j++)
                {
                    var xSeg = j - halfDim;
                    var ySeg = 0.0f;
                    var zSeg = i - halfDim;

                    textures.Add(new Vector2(i, j));
                    normals.Add(new Vector3(xSeg, ySeg, zSeg));
                    positions.Add(new Vector3(xSeg, ySeg, zSeg));
                }
            }

            return DataManipulationService.GetTextureShadedArrayFromVectors(positions, normals, textures)
                .Get1DFrom2D().ToArray();
        }

        private uint[] CreatePlaneIndices()
        {
            var indices = new List<uint>();

            for (var i = 0; i < _dimensions; i++)
            {
                for (var j = 0; j < _dimensions; j++)
                {
                    indices.Add(_dimensions * (uint)i + (uint)j);
                    indices.Add(_dimensions * (uint)i + (uint)j + _dimensions);
                    indices.Add(_dimensions * (uint)i + (uint)j + _dimensions + 1);

                    indices.Add(_dimensions * (uint)i + (uint)j);
                    indices.Add(_dimensions * (uint)i + (uint)j + _dimensions + 1);
                    indices.Add(_dimensions * (uint)i + (uint)j + 1);

                }
            }

            return indices.ToArray();
        }
    }
}
