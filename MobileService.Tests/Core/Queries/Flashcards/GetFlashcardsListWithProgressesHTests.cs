using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MobileService.Core;
using MobileService.Core.Queries.Flashcards;
using MobileService.DataAccess;
using MobileService.DataAccess.Repos;
using MobileService.Tests.MockData;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MobileService.Tests.Core.Queries.Flashcards
{
    [Collection("testdb_impact")]
    public class GetFlashcardsListWithProgressesHTests
    {
        [Fact]
        public async Task TestA()
        {
            var mocker = new MockDataV6();
            mocker.Reset();

            var serviceProvider = new ServiceCollection()
                .AddTransient<ICollectionRepo, CollectionRepo>()
                .AddTransient<IFlashcardRepo, FlashcardRepo>()
                .AddTransient<IFlashcardProgressRepo, FlashcardProgressRepo>()
                .AddDbContext<AppDbContext>(options => options.UseSqlServer(MockDatabaseFactory.DbMockConnectionString))
                .AddMediatR(typeof(MediatREntryPoint).Assembly)
                .BuildServiceProvider();

            var mediator = serviceProvider.GetService<IMediator>();

            var output = await mediator.Send(new GetFlashcardsListWithProgressQ(Guid.Parse("d30c8f79-291b-4532-8f22-b693e61d6bb5"), "9a4e1d79-d64e-4ec4-85e5-53bdef5043f4"));
      
            Assert.Equal(3, output.Count);
        }

        [Fact]
        public async Task TestB()
        {
            var mocker = new MockDataV6();
            mocker.Reset();

            var serviceProvider = new ServiceCollection()
                .AddTransient<ICollectionRepo, CollectionRepo>()
                .AddTransient<IFlashcardProgressRepo, FlashcardProgressRepo>()
                .AddTransient<IFlashcardRepo, FlashcardRepo>()
                .AddDbContext<AppDbContext>(options => options.UseSqlServer(MockDatabaseFactory.DbMockConnectionString))
                .AddMediatR(typeof(MediatREntryPoint).Assembly)
                .BuildServiceProvider();

            var mediator = serviceProvider.GetService<IMediator>();

            var output = await mediator.Send(new GetFlashcardsListWithProgressQ(Guid.Parse("d30c8f79-291b-4532-8f22-b693e61d6bb5"), "9a4e1d79-d64e-4ec4-85e5-53bdef5043f4"));

            var flashcard = output.FirstOrDefault(e => e.Id == Guid.Parse("6aa83ba0-1396-428f-adb7-d7ab972459eb"));

            Assert.NotNull(flashcard);

            Assert.Equal("Foreign 1", flashcard.Foreign);
            Assert.Equal("Native 1", flashcard.Native);

            Assert.Equal(2, flashcard.ProgressForeignToNative.CorrectInRow);
            Assert.Equal(DateTime.Now.Date.AddDays(5).ToString("dd-MM-yyyy"), flashcard.ProgressForeignToNative.PracticeDate);
            Assert.Equal(DateTime.Now.Date.AddDays(5 + 3).ToString("dd-MM-yyyy"), flashcard.ProgressForeignToNative.PracticeDateIfCorrectAns);
        }
    }
}
