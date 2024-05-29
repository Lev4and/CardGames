namespace CardGames.GameLogic
{
    public interface ICardCombinationAnalyzer
    {
        ICardCombinationAnalyzeResult Analyze(ICardCombination combination);
    }
}
