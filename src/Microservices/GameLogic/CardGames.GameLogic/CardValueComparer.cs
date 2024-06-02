namespace CardGames.GameLogic
{
    public class CardValueComparer : ICardValueComparer
    {
        public int Compare(CardValue x, CardValue y)
        {
            return (int)x - (int)y;
        }
    }
}
