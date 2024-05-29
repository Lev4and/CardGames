namespace CardGames.GameLogic
{
    public interface IFactory<T>
    {
        T Create();
    }
}
