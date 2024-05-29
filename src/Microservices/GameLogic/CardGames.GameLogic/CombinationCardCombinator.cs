namespace CardGames.GameLogic
{
    public class CombinationCardCombinator : CombinationCombinator<ICard>, ICombinationCardCombinator
    {
        public CombinationCardCombinator(int maxItemsInCombination) : base(maxItemsInCombination)
        {

        }
    }
}
