using Assimp;
using CardGames.OpenGLGameEngine.Entities.Components;

namespace CardGames.OpenGLGameEngine.Models.Shapes3D.Models
{
    public class Model
    {
        private readonly string _modelFilePath;

        private readonly List<Mesh> _meshes;

        private Scene _scene = null!;

        public Model(string modelFilePath)
        {
            _modelFilePath = modelFilePath;

            _meshes = new List<Mesh>();
        }

        public void Init(Shader shader)
        {
            LoadModel();

            foreach (var mesh in _meshes)
            {
                mesh.BindAndBuffer(shader);
            }
        }

        public void Draw(Shader shader, TransformComponent transform)
        {
            foreach (var mesh in _meshes)
            {
                mesh.Draw(shader, transform);
            }
        }

        private void LoadModel()
        {
            var assimpContext = new AssimpContext();

            _scene = assimpContext.ImportFile(_modelFilePath, PostProcessSteps.Triangulate | PostProcessSteps.FlipUVs);

            foreach (var mesh in _scene.Meshes)
            {
                var vertices = new List<float>();

                for (var i = 0; i < mesh.VertexCount; i++)
                {
                    vertices.Add(mesh.Vertices[i].X);
                    vertices.Add(mesh.Vertices[i].Y);
                    vertices.Add(mesh.Vertices[i].Z);

                    vertices.Add(mesh.Normals[i].X);
                    vertices.Add(mesh.Normals[i].Y);
                    vertices.Add(mesh.Normals[i].Z);

                    vertices.Add(mesh.TextureCoordinateChannels[0][i].X);
                    vertices.Add(mesh.TextureCoordinateChannels[0][i].Y);
                }

                var indices = new List<uint>();

                foreach (var face in mesh.Faces)
                {
                    foreach (var index in face.Indices)
                    {
                        indices.Add((uint)index);
                    }
                }

                _meshes.Add(new Mesh(vertices.ToArray(), indices.ToArray()));
            }
        }
    }
}
