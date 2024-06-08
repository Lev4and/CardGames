namespace CardGames.GameLogic.Tests
{
    public class ShuffledCardCollectionFactoryTests
    {
        [Fact]
        public void Constructor_WithNullParam_ShouldThrowException()
        {
            var action = new Action(() => new ShuffledCardCollectionFactory(null));

            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void Constructor_WithNotNullParam_ShouldCreateNotNullObject()
        {
            var cardIdRange = new CardIdRange(CardConstants.MinId, CardConstants.MaxId);
            var shuffledCardCollectionFactory = new ShuffledCardCollectionFactory(cardIdRange);

            Assert.NotNull(shuffledCardCollectionFactory);
        }

        [Theory]
        [ClassData((typeof(ShuffledCardCollectionFactoryTestsData.CreateWithExpectedLengthResult)))]
        public void Create_ShouldReturnCollectionWithExpectedLength(ICardIdRange cardIdRange,
            int expectedLengthResult)
        {
            var shuffledCardCollectionFactory = new ShuffledCardCollectionFactory(cardIdRange);

            var cardCollection = shuffledCardCollectionFactory.Create()
                .ToList();

            Assert.Equal(expectedLengthResult, cardCollection.Count);
        }
    }
}
