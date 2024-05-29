namespace CardGames.GameLogic
{
    public class Card : ICard
    {
        private readonly ushort _id;

        public CardSuit Suit
        {
            get
            {
                return CalculateSuit();
            }
        }

        public CardColor Color
        {
            get
            {
                return CalculateColor();
            }
        }

        public CardValue Value
        {
            get
            {
                return CalculateValue();
            }
        }

        public Card(ushort id)
        {
            if (id < CardConstants.MinId || id > CardConstants.MaxId)
                throw new ArgumentOutOfRangeException(nameof(id));

            _id = id;
        }

        private CardSuit CalculateSuit()
        {
            var suitValue = _id % CardConstants.SuitCount;

            return !CardConstants.JokersIds.Contains(_id)
                ? Enum.GetValues<CardSuit>().First(suit => (ushort)suit == suitValue)
                : CardSuit.None;
        }

        private CardColor CalculateColor()
        {
            var suit = CalculateSuit();

            if (suit == CardSuit.Hearts || suit == CardSuit.Diamonds ||
                (suit == CardSuit.None && _id % CardConstants.ColorCount == 1))
            {
                return CardColor.Red;
            }
            else if (suit == CardSuit.Clubs || suit == CardSuit.Spades ||
                (suit == CardSuit.None && _id % CardConstants.ColorCount == 0))
            {
                return CardColor.Black;
            }
            else
            {
                throw new ArgumentException(nameof(_id));
            }
        }

        private CardValue CalculateValue()
        {
            var valueValue = (_id - 1) / CardConstants.SuitCount;

            return Enum.GetValues<CardValue>().First(value => (ushort)value == valueValue);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            else return ((Card)obj).ToString() == ToString();
        }

        public override string ToString()
        {
            return $"{Value}{Color}{Suit}";
        }
    }
}
