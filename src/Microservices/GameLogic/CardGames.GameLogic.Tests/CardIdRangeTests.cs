namespace CardGames.GameLogic.Tests
{
    public class CardIdRangeTests
    {
        [Theory]
        [ClassData(typeof(CardIdRangeTestsData.InvalidConstructorParams))]
        public void Constructor_WithInvalidParams_ShouldThrowException(ushort minId, ushort maxId)
        {
            var action = new Action(() =>
            {
                new CardIdRange(minId, maxId);
            });

            Assert.Throws<ArgumentOutOfRangeException>(action);
        }

        [Theory]
        [ClassData(typeof(CardIdRangeTestsData.ValidConstructorParams))]
        public void Constructor_WithValidParams_ShouldCreateNotNullObject(ushort minId, ushort maxId)
        {
            var cardIdRange = new CardIdRange(minId, maxId);

            Assert.NotNull(cardIdRange);
        }

        [Theory]
        [ClassData(typeof(CardIdRangeTestsData.GetAvailableIdsWithExpectedLengthResult))]
        public void GetAvailableIds_ShouldReturnCollectionWithExpectedLength(ushort minId, ushort maxId,
            int expectedLengthResult)
        {
            var cardIdRange = new CardIdRange(minId, maxId);

            var availableIds = cardIdRange.GetAvailableIds()
                .ToList();

            Assert.Equal(availableIds.Count, expectedLengthResult);
        }
    }
}
