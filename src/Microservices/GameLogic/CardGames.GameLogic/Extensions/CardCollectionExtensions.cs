namespace CardGames.GameLogic.Extensions
{
    public static class CardCollectionExtensions
    {
        public static IGrouping<CardValue, ICard>? GetGroupedCardsByMinCardValue(
            this IEnumerable<ICard> cardCollection, ICardValueComparer? cardValueComparer = null)
        {
            return cardCollection.GroupBy(card => card.Value)
                .OrderBy(group => group.Key, cardValueComparer)
                .FirstOrDefault();
        }

        public static IGrouping<CardValue, ICard>? GetGroupedCardsByMaxCardValue(
            this IEnumerable<ICard> cardCollection, ICardValueComparer? cardValueComparer = null)
        {
            return cardCollection.GroupBy(card => card.Value)
                .OrderBy(group => group.Key, cardValueComparer)
                .LastOrDefault();
        }

        public static IEnumerable<IGrouping<CardSuit, ICard>> GetGroupedCardsBySameCardSuit(
            this IEnumerable<ICard> cardCollection)
        {
            return cardCollection.GroupBy(card => card.Suit)
                .Where(group => group.Count() > 1);
        }

        public static IEnumerable<IGrouping<CardValue, ICard>> GetGroupedCardsBySameCardValue(
            this IEnumerable<ICard> cardCollection)
        {
            return cardCollection.GroupBy(card => card.Value)
                .Where(group => group.Count() > 1);
        }

        public static IEnumerable<IGrouping<CardColor, ICard>> GetGroupedCardsBySameCardColor(
            this IEnumerable<ICard> cardCollection)
        {
            return cardCollection.GroupBy(card => card.Color)
                .Where(group => group.Count() > 1);
        }

        public static IEnumerable<IGrouping<CardValue, ICard>> GetGroupedCardsByPairCardValue(
            this IEnumerable<ICard> cardCollection)
        {
            return cardCollection.GroupBy(card => card.Value)
                .Where(group => group.Count() > 1 && group.Count() % 2 == 0);
        }

        public static IEnumerable<IEnumerable<IGrouping<CardValue, ICard>>> GetGroupedCardsByAscOrderCardValue(
            this IEnumerable<ICard> cardCollection, ICardValueComparer? cardValueComparer = null)
        {
            return cardCollection.GetGroupedCardsByOrderCardValue(CardValueOrderDirection.Ascending, cardValueComparer);
        }

        public static IEnumerable<IEnumerable<IGrouping<CardValue, ICard>>> GetGroupedCardsByDescOrderCardValue(
            this IEnumerable<ICard> cardCollection, ICardValueComparer? cardValueComparer = null)
        {
            return cardCollection.GetGroupedCardsByOrderCardValue(CardValueOrderDirection.Descending, cardValueComparer);
        }

        public static IEnumerable<IEnumerable<IGrouping<CardValue, ICard>>> GetGroupedCardsByOrderCardValue(
            this IEnumerable<ICard> cardCollection, CardValueOrderDirection orderDirection = CardValueOrderDirection.Ascending, 
                ICardValueComparer? cardValueComparer = null)
        {
            var result = new List<IEnumerable<IGrouping<CardValue, ICard>>>();

            var validCardValueDelta = orderDirection == CardValueOrderDirection.Ascending ? -1 : 1;

            var sortedGroupedCardsByCardValue = cardCollection.OrderBy(card => card.Value, cardValueComparer)
                .GroupBy(card => card.Value);

            if (orderDirection == CardValueOrderDirection.Descending)
            {
                sortedGroupedCardsByCardValue = sortedGroupedCardsByCardValue.Reverse();
            }

            for (var i = 0; i < sortedGroupedCardsByCardValue.Count(); i += 1)
            {
                var orderedGroupedCardsByCardValue = sortedGroupedCardsByCardValue
                    .Skip(i)
                    .TakeWhile((group, index) =>
                    {
                        var previousGroup = sortedGroupedCardsByCardValue.ElementAt(index - 1);

                        return index == 0 || cardValueComparer?.Compare(group.Key, previousGroup.Key) == validCardValueDelta ||
                            (int)group.Key - (int)previousGroup.Key == validCardValueDelta;
                    })
                    .ToList();

                if (orderedGroupedCardsByCardValue.Count > 1)
                {
                    result.Add(orderedGroupedCardsByCardValue);
                }
            }

            return result;
        }
    }
}
