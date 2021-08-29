using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SixLetterWordChallenge.Application.Services;
using SixLetterWordChallenge.Domain.CombinedWords;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace SixLetterWordChallenge.ConsoleApp
{
    class Program
    {
        async static Task Main(string[] args)
        {
            var services = ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();

            var (filePath, length) = GetArguments(args);

            var mediator = serviceProvider.GetService<IMediator>();
            var result = await mediator.Send(new Application.Features.GetCombinedWords.GetCombinedWordsRequest
            {
                FilePath = filePath,
                Length = length
            });

            if (!result.IsSuccessful)
            {
                Console.WriteLine($"Error occurred: {result.ErrorMessage}");
                Console.ReadLine();
                return;
            }

            foreach (var combinedWord in result.CombinedWords)
            {
                Console.WriteLine(combinedWord);
            }

            Console.ReadLine();
        }

        private static (string, int) GetArguments(string[] args)
        {
            const int defaultLength = 6;

            var filePath = args.Length > 0 ? args[0] : "input.txt";

            if (args.Length < 1)
                return (filePath, defaultLength);

            if (!int.TryParse(args[1], out var length))
                return (filePath, defaultLength);

            return (filePath, length);
        }

        private static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddTransient<IWordReader, FileWordReader>();
            services.AddTransient<IWordCombiner, WordCombiner>();
            services.AddTransient<ICombinationGenerator, CombinationGenerator>();
            services.AddMediatR(typeof(Application.IAssemblyMarker).GetTypeInfo().Assembly);

            return services;
        }
    }
}