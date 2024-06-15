using CardGames.OpenGLGameEngine.Entities.Components;
using CardGames.OpenGLGameEngine.Entities;
using CardGames.OpenGLGameEngine.Models;
using OpenTK.Mathematics;

namespace CardGames.OpenGLGameEngine.Assets.Scripts.Shared.Entities
{
    public class Player
    {
        private Shader _shader;
        private Entity _entity;

        public Player(Shader shader, Entity entity)
        {
            _shader = shader;
            _entity = entity;

            _entity.AddComponent(new PlayerComponent(_shader, new Vector3(0.0f, 0.0f, 0.0f)));
            _entity.GetComponent<PlayerComponent>().ActiveMovementPreset = Enums.MovementPresets.Spectator;
        }
    }
}
