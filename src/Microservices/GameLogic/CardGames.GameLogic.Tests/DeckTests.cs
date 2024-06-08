using Xunit.Sdk;

namespace CardGames.GameLogic.Tests
{
    public class DeckTests
    {
        [Fact]
        public void Constructor_WithNullParam_ShouldThrowException()
        {
            var action = new Action(() => new Deck(null));

            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void Constructor_WithEmptyParam_ShouldCreateEmptyObject()
        {
            var deck = new Deck(Enumerable.Empty<ICard>());

            Assert.Empty(deck);
        }

        [Fact]
        public void Constructor_WithNotEmptyParam_ShouldCreateNotEmptyObject()
        {
            var cards = new List<ICard>()
            {
                new Card(1), 
                new Card(2), 
                new Card(3),
                new Card(4),
                new Card(5)
            };

            var deck = new Deck(cards);

            Assert.NotEmpty(deck);
        }

        [Fact] 
        public void TakeTop_WithoutParamWhenDeckEmpty_ShouldThrowException()
        {
            var deck = new Deck(Enumerable.Empty<ICard>());

            var action = new Action(() => deck.TakeTop());

            Assert.Throws<InvalidOperationException>(action);
        }

        [Fact]
        public void TakeTop_WithoutParamDeckNotEmpty_ShouldReturnNotNullResult()
        {
            var card = new Card(1);
            var deck = new Deck(new List<ICard>() { card, new Card(2), new Card(3) });

            var result = deck.TakeTop();

            Assert.Equal(card, result);
        }

        [Fact]
        public void TakeTop_WithInvalidParamWhenDeckEmpty_ShouldThrowException()
        {
            var deck = new Deck(Enumerable.Empty<ICard>());

            var action = new Action(() => deck.TakeTop(0));

            Assert.Throws<ArgumentOutOfRangeException>(action);
        }

        [Fact]
        public void TakeTop_WithValidParamWhenDeckEmpty_ShouldThrowException()
        {
            var deck = new Deck(Enumerable.Empty<ICard>());

            var action = new Action(() => deck.TakeTop(3));

            Assert.Throws<InvalidOperationException>(action);
        }

        [Fact]
        public void TakeTop_WithValidParamWhenDeckNotEmptyAndWhenParamGreaterThanDeckLength_ShouldThrowException()
        {
            var cards = new List<ICard>() 
            { 
                new Card(1), 
                new Card(2), 
                new Card(3) 
            };

            var deck = new Deck(cards);

            var action = new Action(() => deck.TakeTop(4));

            Assert.Throws<InvalidOperationException>(action);
        }

        [Fact]
        public void TakeTop_WithValidParamWhenDeckNotEmptyAndWhenParamLessOrEqualDeckLength_ShouldReturnNotEmptyResult()
        {
            var cards = new List<ICard>()
            {
                new Card(1),
                new Card(2),
                new Card(3)
            };

            var deck = new Deck(cards);

            var result = deck.TakeTop(3)
                .ToList();

            Assert.Equal(3, result.Count);
        }

        [Fact]
        public void TryTakeTop_WithoutParamWhenDeckEmpty_ShouldReturnFalse()
        {
            var deck = new Deck(Enumerable.Empty<ICard>());

            var result = deck.TryTakeTop(out var takenCard);

            Assert.False(result);

            Assert.Null(takenCard);
        }

        [Fact]
        public void TryTakeTop_WithoutParamDeckNotEmpty_ShouldReturnTrue()
        {
            var card = new Card(1);
            var deck = new Deck(new List<ICard>() { card, new Card(2), new Card(3) });

            var result = deck.TryTakeTop(out var takenCard);

            Assert.True(result);

            Assert.NotNull(takenCard);
        }

        [Fact]
        public void TryTakeTop_WithInvalidParamWhenDeckEmpty_ShouldReturnFalse()
        {
            var deck = new Deck(Enumerable.Empty<ICard>());

            var result = deck.TryTakeTop(0, out var takenCards);

            Assert.False(result);

            Assert.Null(takenCards);
        }

        [Fact]
        public void TryTakeTop_WithValidParamWhenDeckEmpty_ShouldReturnFalse()
        {
            var deck = new Deck(Enumerable.Empty<ICard>());

            var result = deck.TryTakeTop(3, out var takenCards);

            Assert.False(result);

            Assert.Null(takenCards);
        }

        [Fact]
        public void TryTakeTop_WithValidParamWhenDeckNotEmptyAndWhenParamGreaterThanDeckLength_ShouldReturnFalse()
        {
            var cards = new List<ICard>()
            {
                new Card(1),
                new Card(2),
                new Card(3)
            };

            var deck = new Deck(cards);

            var result = deck.TryTakeTop(4, out var takenCards);

            Assert.False(result);

            Assert.Null(takenCards);
        }

        [Fact]
        public void TryTakeTop_WithValidParamWhenDeckNotEmptyAndWhenParamLessOrEqualDeckLength_ShouldReturnTrue()
        {
            var cards = new List<ICard>()
            {
                new Card(1),
                new Card(2),
                new Card(3)
            };

            var deck = new Deck(cards);

            var result = deck.TryTakeTop(3, out var takenCards);

            Assert.True(result);

            Assert.NotEmpty(takenCards);
        }

        [Fact]
        public void PutDown_WithNullParam_ShouldThrowException()
        {
            var cards = new List<ICard>()
            {
                new Card(1),
                new Card(2),
                new Card(3)
            };

            var deck = new Deck(cards);

            var action = new Action(() => deck.PutDown(null));

            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void PutDown_WithNotNullParam_ShouldIncreaseLengthOfDeck()
        {
            var cards = new List<ICard>()
            {
                new Card(1),
                new Card(2),
                new Card(3)
            };

            var deck = new Deck(cards);

            deck.PutDown(new Card(4));

            Assert.NotEqual(deck.Count, cards.Count);
        }
    }
}
