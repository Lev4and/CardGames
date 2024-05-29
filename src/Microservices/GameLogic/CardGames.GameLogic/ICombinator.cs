namespace CardGames.GameLogic
{
    public interface ICombinator<T>
    {
        IReadOnlyCollection<ICombination<T>> Combinate(IEnumerable<T> items);
    }
}
