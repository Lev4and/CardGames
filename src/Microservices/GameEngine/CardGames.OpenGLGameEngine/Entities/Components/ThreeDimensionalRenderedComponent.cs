using CardGames.OpenGLGameEngine.Models;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace CardGames.OpenGLGameEngine.Entities.Components
{
    public abstract class ThreeDimensionalRenderedComponent : Component
    {
        protected readonly Shader _shader;

        protected readonly Vector3? _scale;
        protected readonly Vector3 _position;
        protected readonly Quaternion? _rotation;

        protected TransformComponent? Transform;
        protected List<Texture> Textures;

        public ThreeDimensionalRenderedComponent(Shader shader, Vector3 position, Quaternion? rotation = null, 
            Vector3? scale = null, List<Texture>? textures = null)
        {
            _shader = shader;
            
            _scale = scale;

            _position = position;
            _rotation = rotation;

            Transform = null;
            Textures = textures ?? new List<Texture>();
        }

        public ThreeDimensionalRenderedComponent(Shader shader, TransformComponent transform, 
            List<Texture>? textures = null)
        {
            _shader = shader;

            Transform = transform;
            Textures = textures ?? new List<Texture>();
        }

        public override void Init()
        {
            base.Init();

            BindAndBuffer();

            Transform = Entity.AddComponent(Transform ?? new TransformComponent(_position, _rotation, _scale));
        }

        public override void Draw()
        {
            base.Draw();

            for (var i = 0; i < Textures.Count; i++)
            {
                Textures[i].Bind(TextureUnit.Texture0 + i);
            }

            DrawComp();

            for (var i = 0; i < Textures.Count; i++)
            {
                Textures[i].Bind(TextureUnit.Texture0 + i);

                Textures[i].Unbind();
            }
        }

        public override void Update()
        {
            base.Update();
        }

        public abstract void BindAndBuffer();

        public abstract void DrawComp();
    }
}
