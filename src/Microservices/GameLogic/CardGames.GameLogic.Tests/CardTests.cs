namespace CardGames.GameLogic.Tests
{
    public class CardTests
    {
        [Theory]
        [ClassData((typeof(CardTestsData.InvalidConstructorParams)))]
        public void Constructor_WithInvalidParams_ShouldThrowException(ushort id)
        {
            var action = new Action(() => new Card(id));

            Assert.Throws<ArgumentOutOfRangeException>(action);
        }

        [Theory]
        [ClassData(typeof(CardTestsData.ValidConstructorParams))]
        public void Constructor_WithValidParams_ShouldCreateObjectWithExpectedPropertyValues(ushort id, CardSuit expectedSuit,
            CardColor expectedColor, CardValue expectedValue)
        {
            var card = new Card(id);

            Assert.Equal(expectedSuit, card.Suit);

            Assert.Equal(expectedColor, card.Color);
            Assert.Equal(expectedValue, card.Value);
        }
    }
}
