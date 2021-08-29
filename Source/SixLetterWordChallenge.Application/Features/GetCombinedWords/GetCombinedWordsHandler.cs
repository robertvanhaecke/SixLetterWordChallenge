using MediatR;
using SixLetterWordChallenge.Application.Services;
using SixLetterWordChallenge.Domain.CombinedWords;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SixLetterWordChallenge.Application.Features.GetCombinedWords
{
    public class GetCombinedWordsHandler : IRequestHandler<GetCombinedWordsRequest, GetCombinedWordsResult>
    {
        private readonly IWordReader _wordReader;
        private readonly IWordCombiner _wordCombiner;

        public GetCombinedWordsHandler(IWordReader wordReader, IWordCombiner wordCombiner)
        {
            _wordReader = wordReader;
            _wordCombiner = wordCombiner;
        }

        public Task<GetCombinedWordsResult> Handle(GetCombinedWordsRequest request, CancellationToken cancellationToken)
        {
            var wordReaderResult = _wordReader.Read(request.FilePath);
            if (!wordReaderResult.IsSuccessful)
                return Task.FromResult(GetCombinedWordsResult.Error(wordReaderResult.ErrorMessage));

            if (!wordReaderResult.Words.Any())
                return Task.FromResult(GetCombinedWordsResult.Success(Enumerable.Empty<CombinedWord>()));

            var result = _wordCombiner.Combine(wordReaderResult.Words, request.Length);

            return Task.FromResult(GetCombinedWordsResult.Success(result));
        }
    }
}