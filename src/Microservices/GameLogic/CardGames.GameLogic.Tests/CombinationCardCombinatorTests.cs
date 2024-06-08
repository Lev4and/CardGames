namespace CardGames.GameLogic.Tests
{
    public class CombinationCardCombinatorTests
    {
        [Theory]
        [ClassData(typeof(CombinationCardCombinatorTestsData.InvalidConstructorParams))]
        public void Constructor_WithInvalidParam_ShouldThrowException(int maxItemsInCombination)
        {
            var action = new Action(() => new CombinationCardCombinator(maxItemsInCombination));

            Assert.Throws<ArgumentOutOfRangeException>(action);
        }

        [Theory]
        [ClassData(typeof(CombinationCardCombinatorTestsData.ValidConstructorParams))]
        public void Constructor_WithValidParam_ShouldCreateNotNullObject(int maxItemsInCombination)
        {
            var combinationCardCombinator = new CombinationCardCombinator(maxItemsInCombination);

            Assert.NotNull(combinationCardCombinator);
        }

        [Theory]
        [ClassData(typeof(CombinationCardCombinatorTestsData.CombinateWithExpectedLengthResult))]
        public void Combinate_WithParam_ShouldReturnCombinationsExpectedLength(int maxItemsInCombination,
            IEnumerable<ICard> items, int expectedCombinationsCount)
        {
            var combinationCardCombinator = new CombinationCardCombinator(maxItemsInCombination);

            var combinations = combinationCardCombinator.Combinate(items);

            Assert.Equal(expectedCombinationsCount, combinations.Count);
        }
    }
}
