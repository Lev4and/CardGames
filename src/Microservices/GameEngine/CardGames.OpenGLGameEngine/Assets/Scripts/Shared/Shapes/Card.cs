using CardGames.OpenGLGameEngine.Entities.Components;
using CardGames.OpenGLGameEngine.Entities;
using CardGames.OpenGLGameEngine.Models.Shapes3D.Models;
using CardGames.OpenGLGameEngine.Models;
using OpenTK.Mathematics;

namespace CardGames.OpenGLGameEngine.Assets.Scripts.Shared.Shapes
{
    public class Card
    {
        private Shader _shader;
        private Entity _entity;

        public Vector3 _position;
        public Quaternion? _rotation;
        public Vector2? _scale;

        public Card(Shader shader, Entity entity, string frontTextureFilePath, Vector3 position, 
            Quaternion? rotation = null, Vector2? scale = null)
        {
            _shader = shader;
            _entity = entity;

            _position = position;
            _rotation = rotation;
            _scale = scale;

            _entity.AddComponent(new ModelComponent(_shader,
                new Model($"Assets/Models/Card.dae"),
                position,
                rotation,
                scale != null
                    ? new Vector3(scale.Value.X, 0.01f, scale.Value.Y)
                    : new Vector3(0.1f, 0.1f, 0.1f),
                new Dictionary<string, Texture>
                {
                    {
                        "Side", new Texture("Assets/Textures/CardSideWhite.png")
                    },
                    {
                        "Front", new Texture(frontTextureFilePath)
                    },
                    {
                        "Background", new Texture("Assets/Decks/Second/SecondDeckBack.png")
                    },
                }));

            _entity.AddComponent(new StaticRigidBodyComponent());
        }
    }
}
