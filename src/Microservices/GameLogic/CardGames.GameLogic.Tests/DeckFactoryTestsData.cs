namespace CardGames.GameLogic.Tests
{
    public class DeckFactoryTestsData
    {
        public class CreateWithExpectedLength : TheoryData<ICardCollectionFactory, int>
        {
            public CreateWithExpectedLength()
            {
                Add(new CardCollectionFactory(new CardIdRange(CardConstants.MinId, CardConstants.MaxId)), 54);
                Add(new CardCollectionFactory(new CardIdRange(1, 52)), 52);
            }
        }
    }
}
