namespace CardGames.GameLogic.Tests
{
    public class CardTestsData
    {
        public class InvalidConstructorParams : TheoryData<ushort>
        {
            public InvalidConstructorParams()
            {
                Add(CardConstants.MinId - 1);
                Add(CardConstants.MaxId + 1);
            }
        }

        public class ValidConstructorParams : TheoryData<ushort, CardSuit, CardColor, CardValue>
        {
            public ValidConstructorParams()
            {
                Add(CardConstants.MinId, CardSuit.Hearts, CardColor.Red, CardValue.Two);
                Add(26, CardSuit.Diamonds, CardColor.Red, CardValue.Eight);
                Add(27, CardSuit.Spades, CardColor.Black, CardValue.Eight);
                Add(CardConstants.MaxId, CardSuit.None, CardColor.Black, CardValue.Joker);
            }
        }
    }
}
