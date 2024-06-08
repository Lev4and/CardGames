namespace CardGames.GameLogic.Tests
{
    public class DeckFactoryTests
    {
        [Fact]
        public void Constructor_WithNullParam_ShouldThrowException()
        {
            var action = new Action(() => new DeckFactory(null));

            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void Constructor_WithNotNullParam_ShouldCreateNotNullObject()
        {
            var cardIdRange = new CardIdRange(CardConstants.MinId, CardConstants.MaxId);
            var cardCollectionFactory = new CardCollectionFactory(cardIdRange);

            var deckFactory = new DeckFactory(cardCollectionFactory);

            Assert.NotNull(deckFactory);
        }

        [Theory]
        [ClassData(typeof(DeckFactoryTestsData.CreateWithExpectedLength))]
        public void Create_ShouldReturnDeckExpectedLength(ICardCollectionFactory cardCollectionFactory, 
            int expectedDeckLength)
        {
            var deckFactory = new DeckFactory(cardCollectionFactory);

            var deck = deckFactory.Create();

            Assert.Equal(expectedDeckLength, deck.Count());
        }
    }
}
