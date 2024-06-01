
using OpenTK.Mathematics;

namespace CardGames.GameEngine
{
    public abstract class GameObject : IGameObject
    {
        public Guid Id { get; }
        
        public Vector3 Position { get; set; }

        public GameObject()
        {
            Id = Guid.NewGuid();

            Position = new Vector3();
        }

        public GameObject(Vector3 position)
        {
            Position = position;
        }

        public abstract void Render();
    }
}
