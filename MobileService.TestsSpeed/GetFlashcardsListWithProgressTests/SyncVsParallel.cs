using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MobileService.Core;
using MobileService.Core.Queries.Flashcards;
using MobileService.DataAccess;
using MobileService.DataAccess.Repos;
using MobileService.TestsSpeedGetFlashcardsListWithProgressTests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileService.TestsSpeed.GetFlashcardsListWithProgressTests
{
    public static class SyncVsParallel
    {
        public static List<int> FlashcardsCounts { get; set; } = new List<int>() { 10, 50, 100, 300 , 500, 1000, 2000, 5000, 10000};
        public static async Task Test()
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<ICollectionRepo, CollectionRepo>()
                .AddTransient<IFlashcardRepo, FlashcardRepo>()
                .AddTransient<IFlashcardProgressRepo, FlashcardProgressRepo>()
                .AddDbContext<AppDbContext>(options => options.UseSqlServer(MockDatabaseFactory.DbMockConnectionString))
                .AddMediatR(typeof(MediatREntryPoint).Assembly)
                .BuildServiceProvider();

            var mediator = serviceProvider.GetService<IMediator>();

            foreach (var count in FlashcardsCounts)
            {
                MockDatabase.Mock(count);
                //Console.WriteLine($"Database Mocked with {count}");

                StringBuilder row = new StringBuilder();

                row.Append($"| {count} |");

                var stopwatch = System.Diagnostics.Stopwatch.StartNew();

                for (int i = 0; i < 5; i++)
                    await mediator.Send(new GetFlashcardsListWithProgressQ(Guid.Parse("a36d18ca-a912-49da-bfc2-2a41b1d87e7c"), "be723b31-56b0-4b6d-a7c3-b0d979ed4d00"));

                stopwatch.Stop();

                //Console.WriteLine($"(SYNC) Quantity: {count} Time: {stopwatch.ElapsedMilliseconds} miliseconds.");
                row.Append($" {stopwatch.ElapsedMilliseconds / 5} |");

                var stopwatchAsync = System.Diagnostics.Stopwatch.StartNew();

                for (int i = 0; i < 5; i++)
                    await mediator.Send(new GetFlashcardsListWithProgressesParallelQ(Guid.Parse("a36d18ca-a912-49da-bfc2-2a41b1d87e7c"), "be723b31-56b0-4b6d-a7c3-b0d979ed4d00"));

                stopwatchAsync.Stop();

                //Console.WriteLine($"(ASYNC) Quantity: {count} Time: {stopwatchAsync.ElapsedMilliseconds} miliseconds.");
                row.Append($" {stopwatchAsync.ElapsedMilliseconds / 5} |");
                Console.WriteLine(row);
            }
        }
    }
}
