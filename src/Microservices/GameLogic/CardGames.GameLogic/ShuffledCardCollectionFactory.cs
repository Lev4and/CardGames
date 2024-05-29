namespace CardGames.GameLogic
{
    public class ShuffledCardCollectionFactory : ICardCollectionFactory
    {
        private readonly Random _random;
        private readonly ICardIdRange _range;

        public ShuffledCardCollectionFactory(ICardIdRange range)
        {
            _range = range;
            _random = new Random();
        }

        public ICardCollection Create()
        {
            var cards = new List<ICard>();

            var cardIds = _range.GetAvailableIds()
                .ToList();

            while (cardIds.Count > 0)
            {
                var cardId = cardIds.ElementAt(_random.Next(0, cardIds.Count));

                cards.Add(new Card(cardId));

                cardIds.Remove(cardId);
            }

            return new CardCollection(cards);
        }
    }
}
