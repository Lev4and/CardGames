namespace CardGames.GameLogic
{
    public class Grouping<TKey, TElement> : List<TElement>, IGrouping<TKey, TElement>
    {
        public TKey Key { get; }

        public Grouping(TKey key, IEnumerable<TElement> items) : base(items)
        {
            Key = key;
        }
    }
}
