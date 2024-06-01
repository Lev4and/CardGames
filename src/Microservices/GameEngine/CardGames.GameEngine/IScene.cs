namespace CardGames.GameEngine
{
    public interface IScene : IDisposable
    {
        ICamera Camera { get; }

        void Load();

        void AddObject(IGameObject gameObject);

        IGameObject? FindObjectById(Guid id);

        void RemoveObject(IGameObject gameObject);

        void Render();

        void Unload();
    }
}
