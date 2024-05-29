namespace CardGames.GameLogic
{
    public interface ICardIdRange
    {
        IReadOnlyCollection<ushort> GetAvailableIds();
    }
}
