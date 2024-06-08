namespace CardGames.GameLogic
{
    public abstract class CardCollectionFactoryDecorator : ICardCollectionFactory
    {
        protected readonly ICardCollectionFactory _decoratee;

        public CardCollectionFactoryDecorator(ICardCollectionFactory decoratee)
        {
            ArgumentNullException.ThrowIfNull(decoratee, nameof(decoratee));

            _decoratee = decoratee;
        }

        public abstract ICardCollection Create();
    }
}
