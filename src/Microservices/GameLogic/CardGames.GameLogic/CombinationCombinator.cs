using Combinatorics.Collections;

namespace CardGames.GameLogic
{
    public class CombinationCombinator<T> : ICombinationCombinator<T>
    {
        public int MaxItemsInCombination { get; }

        public CombinationCombinator(int maxItemsInCombination)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxItemsInCombination);

            MaxItemsInCombination = maxItemsInCombination;
        }

        public IReadOnlyCollection<ICombination<T>> Combinate(IEnumerable<T> items)
        {
            var combinations = new List<ICombination<T>>();

            if (!items.Any())
            {
                return combinations;
            }

            if (items.Count() > MaxItemsInCombination)
            {
                var generatedCombinations = new Combinations<T>(items, MaxItemsInCombination);

                foreach (var generatedCombination in generatedCombinations)
                {
                    combinations.Add(new Combination<T>(generatedCombination));
                }
            }
            else
            {
                combinations.Add(new Combination<T>(items));
            }

            return combinations;
        }
    }
}
