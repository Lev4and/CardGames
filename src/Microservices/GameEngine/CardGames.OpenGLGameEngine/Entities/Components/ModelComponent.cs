using CardGames.OpenGLGameEngine.Models.Shapes3D.Models;
using CardGames.OpenGLGameEngine.Models;
using OpenTK.Mathematics;

namespace CardGames.OpenGLGameEngine.Entities.Components
{
    public class ModelComponent : ThreeDimensionalRenderedComponent
    {
        private readonly Model _model;
        private readonly Dictionary<string, Texture>? _materialTextures;

        public ModelComponent(Shader shader, Model model, Vector3 position, Quaternion? rotation = null, 
            Vector3? scale = null, Dictionary<string, Texture>? materialTextures = null) : base(shader, position, rotation, scale)
        {
            _model = model;
            _materialTextures = materialTextures;
        }

        public override void BindAndBuffer()
        {
            _model.Init(_shader);
        }

        public override void DrawComp()
        {
            _model.Draw(_shader, Transform!, _materialTextures);
        }
    }
}
