namespace CardGames.GameLogic.Tests
{
    public class CardIdRangeTestsData
    {
        public class InvalidConstructorParams : TheoryData<ushort, ushort>
        {
            public InvalidConstructorParams()
            {
                Add(CardConstants.MinId - 1, CardConstants.MaxId);
                Add(28, 27);
                Add(CardConstants.MinId, CardConstants.MaxId + 1);
                Add(26, 25);
            }
        }

        public class ValidConstructorParams : TheoryData<ushort, ushort>
        {
            public ValidConstructorParams()
            {
                Add(CardConstants.MinId, CardConstants.MaxId);
                Add(1, 52);
            }
        }

        public class GetAvailableIdsWithExpectedLengthResult : TheoryData<ushort, ushort, int>
        {
            public GetAvailableIdsWithExpectedLengthResult()
            {
                Add(CardConstants.MinId, CardConstants.MaxId, 54);
                Add(1, 52, 52);
            }
        }
    }
}
