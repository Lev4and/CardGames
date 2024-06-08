namespace CardGames.GameLogic.Tests
{
    public class CardCollectionFactoryTestsData
    {
        public class CreateWithExpectedLengthResult : TheoryData<ICardIdRange, int>
        {
            public CreateWithExpectedLengthResult()
            {
                Add(new CardIdRange(CardConstants.MinId, CardConstants.MaxId), 54);
                Add(new CardIdRange(1, 52), 52);
            }
        }
    }
}
