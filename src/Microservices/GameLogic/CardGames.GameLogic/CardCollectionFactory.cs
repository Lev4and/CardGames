namespace CardGames.GameLogic
{
    public class CardCollectionFactory : ICardCollectionFactory
    {
        private readonly ICardIdRange _range;

        public CardCollectionFactory(ICardIdRange range)
        {
            _range = range;
        }

        public ICardCollection Create()
        {
            var cards = _range.GetAvailableIds()
                .Select(cardId => new Card(cardId))
                .ToList();

            return new CardCollection(cards);
        }
    }
}
