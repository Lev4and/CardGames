using OpenTK.Mathematics;

namespace CardGames.GameEngine
{
    public interface IGameObject
    {
        Guid Id { get; }

        Vector3 Position { get; set; }

        void Render();
    }
}
