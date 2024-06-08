namespace CardGames.GameLogic.Tests
{
    public class ShuffledCardCollectionFactoryDecoratorTests
    {
        [Fact]
        public void Constructor_WithNullParam_ShouldThrowException()
        {
            var action = new Action(() => new ShuffledCardCollectionFactoryDecorator(null));

            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void Constructor_WithNotNullParam_ShouldCreateNotNullObject()
        {
            var cardCollectionFactory = new CardCollectionFactory(new CardIdRange(CardConstants.MinId, CardConstants.MaxId));
            var shuffledCardCollectionFactory = new ShuffledCardCollectionFactoryDecorator(cardCollectionFactory);

            Assert.NotNull(shuffledCardCollectionFactory);
        }

        [Theory]
        [ClassData((typeof(ShuffledCardCollectionFactoryDecoratorTestsData.CreateWithExpectedLengthResult)))]
        public void Create_ShouldReturnCollectionWithExpectedLength(ICardIdRange cardIdRange,
            int expectedLengthResult)
        {
            var cardCollectionFactory = new CardCollectionFactory(cardIdRange);
            var shuffledCardCollectionFactory = new ShuffledCardCollectionFactoryDecorator(cardCollectionFactory);

            var cardCollection = shuffledCardCollectionFactory.Create()
                .ToList();

            Assert.Equal(expectedLengthResult, cardCollection.Count);
        }
    }
}
