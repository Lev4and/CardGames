namespace CardGames.GameLogic.Tests
{
    public class CardValueComparerTests
    {
        private readonly CardValueComparer _comparer;

        public CardValueComparerTests()
        {
            _comparer = new CardValueComparer();
        }

        [Theory]
        [ClassData(typeof(CardValueComparerTestsData.CompareWithExpectedResult))]
        public void Compare_WithParams_ShouldReturnExpectedResult(CardValue x, CardValue y, 
            int expectedResult)
        {
            var result = _comparer.Compare(x, y);

            Assert.Equal(expectedResult, result);
        }
    }
}
