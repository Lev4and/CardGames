using CardGames.OpenGLGameEngine.Entities.Components;
using CardGames.OpenGLGameEngine.Entities;
using CardGames.OpenGLGameEngine.Models.Shapes3D.Models;
using CardGames.OpenGLGameEngine.Models;
using OpenTK.Mathematics;

namespace CardGames.OpenGLGameEngine.Assets.Scripts.Shared.Shapes
{
    public class Table
    {
        private Shader _shader;
        private Entity _entity;

        public Vector3 Position;
        public Quaternion? Rotation;
        public Vector2? Scale;

        public Table(Shader shader, Entity entity, Vector3 position,
            Quaternion? rotation = null, Vector2? scale = null)
        {
            _shader = shader;
            _entity = entity;

            Position = position;
            Rotation = rotation;
            Scale = scale;

            _entity.AddComponent(new ModelComponent(_shader,
                new Model($"Assets/Models/Table.dae"),
                position,
                rotation,
                scale != null
                    ? new Vector3(scale.Value.X, 0.01f, scale.Value.Y)
                    : new Vector3(5.0f, 5.0f, 5.0f),
                new Dictionary<string, Texture>
                {
                    {
                        "Wood", new Texture("Assets/Textures/TableWood.png")
                    },
                    {
                        "Cloth", new Texture("Assets/Textures/TableCloth.png")
                    },
                    {
                        "Cushion", new Texture("Assets/Textures/TableCushion.png")
                    },
                }));
        }
    }
}
