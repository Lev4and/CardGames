namespace CardGames.GameLogic.Tests
{
    public class CardCollectionFactoryTests
    {
        [Fact]
        public void Constructor_WithNullParam_ShouldThrowException()
        {
            var action = new Action(() => new CardCollectionFactory(null));

            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void Constructor_WithNotNullParam_ShouldCreateNotNullObject()
        {
            var cardIdRange = new CardIdRange(CardConstants.MinId, CardConstants.MaxId);
            var cardCollectionFactory = new CardCollectionFactory(cardIdRange);

            Assert.NotNull(cardCollectionFactory);
        }

        [Theory]
        [ClassData((typeof(CardCollectionFactoryTestsData.CreateWithExpectedLengthResult)))]
        public void Create_ShouldReturnCollectionWithExpectedLength(ICardIdRange cardIdRange, 
            int expectedLengthResult)
        {
            var cardCollectionFactory = new CardCollectionFactory(cardIdRange);

            var cardCollection = cardCollectionFactory.Create()
                .ToList();

            Assert.Equal(expectedLengthResult, cardCollection.Count);
        }
    }
}
