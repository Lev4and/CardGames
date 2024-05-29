namespace CardGames.GameLogic
{
    public class CardCollection : List<ICard>, ICardCollection
    {
        public CardCollection(IEnumerable<ICard> cards) : base(cards)
        {

        }
    }
}
