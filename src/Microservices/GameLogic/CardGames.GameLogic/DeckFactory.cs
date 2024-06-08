namespace CardGames.GameLogic
{
    public class DeckFactory : IDeckFactory
    {
        private readonly ICardCollectionFactory _factory;

        public DeckFactory(ICardCollectionFactory factory)
        {
            ArgumentNullException.ThrowIfNull(factory, nameof(factory));

            _factory = factory;
        }

        public IDeck Create()
        {
            return new Deck(_factory.Create());
        }
    }
}
