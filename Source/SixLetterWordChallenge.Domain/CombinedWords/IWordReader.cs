using SixLetterWordChallenge.Domain.CombinedWords;
using System.Collections.Generic;

namespace SixLetterWordChallenge.Application.Services
{
    public interface IWordReader
    {
        public WordReaderResult Read(string source);
    }

    public class WordReaderResult
    {
        public bool IsSuccessful { get; }
        public string ErrorMessage { get; }
        public IList<Word> Words { get; }

        private WordReaderResult(bool isSuccessful, string errorMessage, IList<Word> words)
        {
            IsSuccessful = isSuccessful;
            ErrorMessage = errorMessage;
            Words = words;
        }

        public static WordReaderResult Success(IList<Word> words) =>
            new(true, null, words);

        public static WordReaderResult Error(string errorMessage) =>
            new(false, errorMessage, null);
    }
}