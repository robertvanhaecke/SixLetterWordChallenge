using SixLetterWordChallenge.Domain.CombinedWords;
using System.IO;
using System.Linq;

namespace SixLetterWordChallenge.Application.Services
{
    public class FileWordReader : IWordReader
    {
        public WordReaderResult Read(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
                return WordReaderResult.Error("File path is invalid");

            if (!File.Exists(source))
                return WordReaderResult.Error("File does not exist");

            var words = File.ReadLines(source)
                .Select(line => Word.Create(line))
                .ToList();

            return WordReaderResult.Success(words);
        }
    }
}