namespace CardGames.GameLogic
{
    public class CardCombination : Combination<ICard>, ICardCombination
    {
        public CardCombination(IEnumerable<ICard> cards) : base(cards)
        {

        }
    }
}
