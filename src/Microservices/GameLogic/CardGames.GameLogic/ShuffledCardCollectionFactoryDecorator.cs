namespace CardGames.GameLogic
{
    public class ShuffledCardCollectionFactoryDecorator : CardCollectionFactoryDecorator
    {
        private readonly Random _random;

        public ShuffledCardCollectionFactoryDecorator(ICardCollectionFactory decoratee) : base(decoratee)
        {
            _random = new Random();
        }

        public override ICardCollection Create()
        {
            var cardCollection = _decoratee.Create().ToList();

            var shuffledCardCollection = new List<ICard>();

            while (cardCollection.Count > 0)
            {
                var card = cardCollection.ElementAt(_random.Next(0, cardCollection.Count));

                shuffledCardCollection.Add(card);

                cardCollection.Remove(card);
            }

            return new CardCollection(shuffledCardCollection);
        }
    }
}
