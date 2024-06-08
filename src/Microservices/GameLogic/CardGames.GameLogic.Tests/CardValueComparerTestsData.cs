namespace CardGames.GameLogic.Tests
{
    public class CardValueComparerTestsData
    {
        public class CompareWithExpectedResult : TheoryData<CardValue, CardValue, int>
        {
            public CompareWithExpectedResult()
            {
                Add(CardValue.Two, CardValue.Joker, -13);
                Add(CardValue.Eight, CardValue.Nine, -1);
                Add(CardValue.Eight, CardValue.Eight, 0);
                Add(CardValue.Nine, CardValue.Eight, 1);
                Add(CardValue.Joker, CardValue.Two, 13);
            }
        }
    }
}
