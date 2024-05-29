namespace CardGames.GameLogic
{
    public interface ICombinationCombinator<T> : ICombinator<T>
    {
        int MaxItemsInCombination { get; }
    }
}
