using MediatR;

namespace SixLetterWordChallenge.Application.Features.GetCombinedWords
{
    public class GetCombinedWordsRequest : IRequest<GetCombinedWordsResult>
    {
        public string FilePath { get; set; }
        public int Length { get; set; }
    }
}