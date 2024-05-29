namespace CardGames.GameLogic
{
    public class Combination<T> : List<T>, ICombination<T>
    {
        public Combination(IEnumerable<T> items) : base(items)
        {

        }
    }
}
