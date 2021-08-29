using System.Collections.Generic;
using System.Linq;

namespace SixLetterWordChallenge.Domain.CombinedWords
{
    public interface IWordCombiner
    {
        IEnumerable<CombinedWord> Combine(IList<Word> words, int length);
    }

    public class WordCombiner : IWordCombiner
    {
        private readonly ICombinationGenerator _combinationGenerator;

        public WordCombiner(ICombinationGenerator combinationGenerator)
        {
            _combinationGenerator = combinationGenerator;
        }

        public IEnumerable<CombinedWord> Combine(IList<Word> words, int length)
        {
            var validWords = words.Where(w => w.Length == length).ToList();
            if (!validWords.Any())
                yield break;

            var combinations = _combinationGenerator.Generate(words, length);
            if (!combinations.Any())
                yield break;

            foreach (var combination in combinations)
            {
                if (validWords.Any(w => w.Value == combination.Value))
                    yield return combination;
            }
        }
    }
}