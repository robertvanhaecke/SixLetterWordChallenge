using SixLetterWordChallenge.Domain.CombinedWords;
using System.Collections.Generic;

namespace SixLetterWordChallenge.Application.Features.GetCombinedWords
{
    public class GetCombinedWordsResult
    {
        public bool IsSuccessful { get; }
        public string ErrorMessage { get; }
        public IEnumerable<CombinedWord> CombinedWords { get; }

        private GetCombinedWordsResult(bool isSuccessful, string errorMessage, IEnumerable<CombinedWord> combinedWords)
        {
            IsSuccessful = isSuccessful;
            ErrorMessage = errorMessage;
            CombinedWords = combinedWords;
        }

        public static GetCombinedWordsResult Success(IEnumerable<CombinedWord> combinedWords) =>
            new(true, null, combinedWords);

        public static GetCombinedWordsResult Error(string errorMessage) =>
            new(false, errorMessage, null);
    }
}