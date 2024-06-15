using CardGames.GameLogic;
using CardGames.OpenGLGameEngine.Entities.Components;
using CardGames.OpenGLGameEngine.Entities;
using CardGames.OpenGLGameEngine.Models.Shapes3D.Models;
using CardGames.OpenGLGameEngine.Models;
using OpenTK.Mathematics;

namespace CardGames.OpenGLGameEngine.Assets.Scripts.Shared.Shapes
{
    public class Chip
    {
        private Shader _shader;
        private Entity _entity;

        public Vector3 Position;
        public Quaternion? Rotation;
        public Vector2? Scale;

        public Chip(Shader shader, Entity entity, string chipColor1, string chipColor2, Vector3 position,
            Quaternion? rotation = null, Vector2? scale = null)
        {
            _shader = shader;
            _entity = entity;

            Position = position;
            Rotation = rotation;
            Scale = scale;

            _entity.AddComponent(new ModelComponent(_shader,
                new Model($"Assets/Models/Chip.dae"),
                position,
                rotation,
                scale != null
                    ? new Vector3(scale.Value.X, 0.01f, scale.Value.Y)
                    : new Vector3(0.1f, 0.1f, 0.1f),
                new Dictionary<string, Texture>
                {
                    {
                        "Color_1", new Texture($"Assets/Textures/{"Chip"}{chipColor1}.png")
                    },
                    {
                        "Color_2", new Texture($"Assets/Textures/{"Chip"}{chipColor2}.png")
                    },
                }));
        }
    }
}
