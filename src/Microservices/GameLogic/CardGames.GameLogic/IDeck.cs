namespace CardGames.GameLogic
{
    public interface IDeck : ICardCollection
    {
        ICard TakeTop();

        IEnumerable<ICard> TakeTop(int count);

        bool TryTakeTop(out ICard? card);

        bool TryTakeTop(int count, out IEnumerable<ICard>? card);

        void PutDown(ICard card);
    }
}
