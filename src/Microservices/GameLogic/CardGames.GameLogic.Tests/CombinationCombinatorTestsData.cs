namespace CardGames.GameLogic.Tests
{
    public class CombinationCombinatorTestsData
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

        public class CombinateWithExpectedLengthResult : TheoryData<int, IEnumerable<int>, int>
        {
            public CombinateWithExpectedLengthResult()
            {
                Add(3, new List<int>() { 1, 2 }, 1);
                Add(3, new List<int>() { 1, 2, 3 }, 1);
                Add(2, new List<int>() { 1, 2, 3 }, 3);
                Add(2, new List<int>() { 1, 1, 1 }, 3);
            }
        }
    }
}
