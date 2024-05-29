namespace CardGames.GameLogic
{
    public class CardIdRange : ICardIdRange
    {
        private readonly IReadOnlyCollection<ushort> _availableIds;

        public CardIdRange(ushort minId, ushort maxId)
        {
            if (minId < CardConstants.MinId || minId > CardConstants.MaxId || minId > maxId)
                throw new ArgumentOutOfRangeException(nameof(minId));

            if (maxId < CardConstants.MinId || maxId > CardConstants.MaxId || maxId < minId)
                throw new ArgumentOutOfRangeException(nameof(maxId));

            var availableIds = new List<ushort>();

            for (var id = minId; id <= maxId; id += 1)
            {
                availableIds.Add(id);
            }

            _availableIds = availableIds;
        }

        public IReadOnlyCollection<ushort> GetAvailableIds()
        {
            return _availableIds;
        }
    }
}
