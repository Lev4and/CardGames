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

        public void Draw(Shader shader, TransformComponent transform, Dictionary<string, Texture>? materialTextures)
        {
            foreach (var mesh in _meshes)
            {
                if (materialTextures is not null && materialTextures.ContainsKey(mesh.Name))
                {
                    materialTextures[mesh.Name].Bind(OpenTK.Graphics.OpenGL.TextureUnit.Texture0);
                }

                mesh.Draw(shader, transform);

                if (materialTextures is not null && materialTextures.ContainsKey(mesh.Name))
                {
                    materialTextures[mesh.Name].Unbind(OpenTK.Graphics.OpenGL.TextureUnit.Texture0);
                }
            }
        }

        private void LoadModel()
        {
            var assimpContext = new AssimpContext();

            _scene = assimpContext.ImportFile(_modelFilePath, PostProcessSteps.Triangulate | PostProcessSteps.FlipUVs | PostProcessSteps.GenerateSmoothNormals | PostProcessSteps.CalculateTangentSpace | PostProcessSteps.ImproveCacheLocality | PostProcessSteps.SortByPrimitiveType);

            for (var i = 0; i < _scene.MeshCount; i += 1)
            {
                var mesh = _scene.Meshes.ElementAt(i);

                var vertices = new List<float>();

                for (var j = 0; j < mesh.VertexCount; j++)
                {
                    vertices.Add(mesh.Vertices[j].X);
                    vertices.Add(mesh.Vertices[j].Y);
                    vertices.Add(mesh.Vertices[j].Z);

                    vertices.Add(mesh.Normals[j].X);
                    vertices.Add(mesh.Normals[j].Y);
                    vertices.Add(mesh.Normals[j].Z);

                    vertices.Add(mesh.TextureCoordinateChannels[0][j].X);
                    vertices.Add(1f - mesh.TextureCoordinateChannels[0][j].Y);
                }

                var indices = new List<uint>();

                foreach (var face in mesh.Faces)
                {
                    foreach (var index in face.Indices)
                    {
                        indices.Add((uint)index);
                    }
                }

                var meshName = _scene.Materials.ElementAtOrDefault(i)?.Name ?? string.Empty;

                _meshes.Add(new Mesh(meshName, vertices.ToArray(), indices.ToArray()));
            }
        }
    }
}
