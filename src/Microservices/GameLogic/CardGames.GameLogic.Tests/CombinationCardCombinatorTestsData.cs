namespace CardGames.GameLogic.Tests
{
    public class CombinationCardCombinatorTestsData
    {
        public class InvalidConstructorParams : TheoryData<int>
        {
            public InvalidConstructorParams()
            {
                Add(-1);
                Add(0);
            }
        }

        public class ValidConstructorParams : TheoryData<int>
        {
            public ValidConstructorParams()
            {
                Add(1);
                Add(5);
            }
        }

        public class CombinateWithExpectedLengthResult : TheoryData<int, IEnumerable<ICard>, int>
        {
            public CombinateWithExpectedLengthResult()
            {
                Add(3, new List<ICard>() { new Card(1), new Card(2) }, 1);
                Add(3, new List<ICard>() { new Card(1), new Card(2), new Card(3) }, 1);
                Add(2, new List<ICard>() { new Card(1), new Card(2), new Card(3) }, 3);
                Add(2, new List<ICard>() { new Card(1), new Card(1), new Card(1) }, 3);
            }
        }
    }
}
