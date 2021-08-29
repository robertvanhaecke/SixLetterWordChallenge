using System;
using System.Collections.Generic;
using System.Linq;

namespace SixLetterWordChallenge.Domain.CombinedWords
{
    public interface ICombinationGenerator
    {
        IEnumerable<CombinedWord> Generate(IList<Word> words, int length);
    }

    public class CombinationGenerator : ICombinationGenerator
    {
        public IEnumerable<CombinedWord> Generate(IList<Word> words, int length)
        {
            if (length <= 1)
                throw new ArgumentException("Length must be a greater than 1", nameof(length));

            return GenerateInternal(words, length);
        }

        private static IEnumerable<CombinedWord> GenerateInternal(IList<Word> words, int length)
        {
            var combinedWords = words.Select(word => CombinedWord.Create(word));

            while (combinedWords.Any())
            {
                var combinations = combinedWords.SelectMany(word => GenerateCombinations(word, words, length));

                foreach (var result in combinations.Where(c => c.Length == length))
                    yield return result;

                combinedWords = combinations.Where(c => c.Length < length);
            }
        }

        private static IEnumerable<CombinedWord> GenerateCombinations(CombinedWord combinedWord, IList<Word> originalWords, int length) =>
            originalWords
                .Select(originalWord => CombinedWord.Combine(combinedWord, originalWord))
                .Where(w => w.Length <= length);
    }
}