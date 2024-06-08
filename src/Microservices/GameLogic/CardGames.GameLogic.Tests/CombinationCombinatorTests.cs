namespace CardGames.GameLogic.Tests
{
    public class CombinationCombinatorTests
    {
        [Theory]
        [ClassData(typeof(CombinationCombinatorTestsData.InvalidConstructorParams))]
        public void Constructor_WithInvalidParam_ShouldThrowException(int maxItemsInCombination)
        {
            var action = new Action(() => new CombinationCombinator<int>(maxItemsInCombination));

            Assert.Throws<ArgumentOutOfRangeException>(action);
        }

        [Theory]
        [ClassData(typeof(CombinationCombinatorTestsData.ValidConstructorParams))]
        public void Constructor_WithValidParam_ShouldCreateNotNullObject(int maxItemsInCombination)
        {
            var combinationCombinator = new CombinationCombinator<int>(maxItemsInCombination);

            Assert.NotNull(combinationCombinator);
        }

        [Theory]
        [ClassData(typeof(CombinationCombinatorTestsData.CombinateWithExpectedLengthResult))]
        public void Combinate_WithParam_ShouldReturnCombinationsExpectedLength(int maxItemsInCombination, 
            IEnumerable<int> items, int expectedCombinationsCount)
        {
            var combinationCombinator = new CombinationCombinator<int>(maxItemsInCombination);

            var combinations = combinationCombinator.Combinate(items);

            Assert.Equal(expectedCombinationsCount, combinations.Count);
        }
    }
}
