using System;
using System.Collections.Generic;
using System.Linq;

namespace SixLetterWordChallenge.Domain.CombinedWords
{
    public record CombinedWord
    {
        public IList<Word> Words { get; }
        public string Value { get; }
        public int Length => Value.Length;

        private CombinedWord(IEnumerable<Word> words)
        {
            Words = words.ToList();
            Value = string.Join(string.Empty, Words.Select(w => w.Value));
        }

        public static CombinedWord Create(params Word[] words)
        {
            words = words ?? throw new ArgumentNullException(nameof(words));
            if (!words.Any()) throw new ArgumentException("At least one word is needed to create a CombinedWord", nameof(words));

            return new CombinedWord(words);
        }

        public static CombinedWord Combine(CombinedWord combinedWord, Word word)
        {
            combinedWord = combinedWord ?? throw new ArgumentNullException(nameof(combinedWord));
            word = word ?? throw new ArgumentNullException(nameof(word));

            var words = combinedWord.Words.Concat(new[] { word });
            return new CombinedWord(words);
        }

        public override string ToString() => 
            $"{string.Join("+", Words.Select(w => w.Value))}={Value}";
    }
}