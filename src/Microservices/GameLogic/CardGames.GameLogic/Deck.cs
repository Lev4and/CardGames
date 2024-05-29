namespace CardGames.GameLogic
{
    public class Deck : Queue<ICard>, IDeck
    {
        public Deck(IEnumerable<ICard> cards) : base(cards)
        {

        }

        public ICard TakeTop()
        {
            return Dequeue();
        }

        public IEnumerable<ICard> TakeTop(int count)
        {
            var result = new List<ICard>();

            for (var i = 0; i < count; i += 1)
            {
                result.Add(TakeTop());
            }

            return result;
        }

        public bool TryTakeTop(out ICard? card)
        {
            return TryDequeue(out card);
        }

        public bool TryTakeTop(int count, out IEnumerable<ICard>? card)
        {
            card = null;

            try
            {
                card = TakeTop(count);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public void PutDown(ICard card)
        {
            Enqueue(card);
        }
    }
}
