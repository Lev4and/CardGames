namespace CardGames.GameLogic.Tests
{
    public class CardCombinationTests
    {
        [Fact]
        public void Constructor_WithNullParam_ShouldThrowException()
        {
            var action = new Action(() => new CardCombination(null));

            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void Constructor_WithEmptyParam_ShouldCreateEmptyObject()
        {
            var combination = new CardCombination(Enumerable.Empty<ICard>());

            Assert.Empty(combination);
        }

        [Fact]
        public void Constructor_WithNotEmptyParam_ShouldCreateNotEmptyObject()
        {
            var items = new List<ICard>() 
            {
                new Card(CardConstants.MinId),
                new Card(CardConstants.MaxId),
            };

            var combination = new CardCombination(items);

            Assert.NotEmpty(combination);
        }
    }
}
