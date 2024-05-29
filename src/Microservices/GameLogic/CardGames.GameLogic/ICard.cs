namespace CardGames.GameLogic
{
    public interface ICard
    {
        CardSuit Suit { get; }

        CardColor Color { get; }

        CardValue Value { get; }
    }
}
