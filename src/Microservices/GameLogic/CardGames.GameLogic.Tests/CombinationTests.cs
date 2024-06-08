namespace CardGames.GameLogic.Tests
{
    public class CombinationTests
    {
        [Fact]
        public void Constructor_WithNullParam_ShouldThrowException()
        {
            var action = new Action(() => new Combination<int>(null));

            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void Constructor_WithEmptyParam_ShouldCreateEmptyObject()
        {
            var combination = new Combination<int>(Enumerable.Empty<int>());

            Assert.Empty(combination);
        }

        [Fact]
        public void Constructor_WithNotEmptyParam_ShouldCreateNotEmptyObject()
        {
            var items = new List<int>() { 1, 2, 3 };

            var combination = new Combination<int>(items);

            Assert.NotEmpty(combination);
        }
    }
}
