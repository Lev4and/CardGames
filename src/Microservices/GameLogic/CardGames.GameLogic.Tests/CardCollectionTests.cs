namespace CardGames.GameLogic.Tests
{
    public class CardCollectionTests
    {
        [Fact]
        public void Constructor_WithNullParam_ShouldThrowException()
        {
            var action = new Action(() => new CardCollection(null));

            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void Constructor_WithEmptyParam_ShouldCreateNotNullObject()
        {
            var cardCollection = new CardCollection(Enumerable.Empty<Card>());

            Assert.NotNull(cardCollection);
        }

        [Fact]
        public void Constructor_WithParam_ShouldCreateNotEmptyObject()
        {
            var cards = new List<ICard>()
            {
                new Card(CardConstants.MinId),
                new Card(CardConstants.MaxId)
            };

            var cardCollection = new CardCollection(cards);

            Assert.NotEmpty(cardCollection);
        }
    }
}
