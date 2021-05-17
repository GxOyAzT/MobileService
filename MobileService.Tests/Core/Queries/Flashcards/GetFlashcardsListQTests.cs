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
    public class GetFlashcardsListQTests
    {
        [Fact]
        public async Task TestA()
        {
            var mocker = new MockDataV2();
            mocker.Reset();

            var serviceProvider = new ServiceCollection()
                .AddTransient<ICollectionRepo, CollectionRepo>()
                .AddTransient<IFlashcardRepo, FlashcardRepo>()
                .AddDbContext<AppDbContext>(options => options.UseSqlServer(MockDatabaseFactory.DbMockConnectionString))
                .AddMediatR(typeof(MediatREntryPoint).Assembly)
                .BuildServiceProvider();

            var mediator = serviceProvider.GetService<IMediator>();

            var getFlashcardsListQ = new GetFlashcardsListQ(Guid.Parse("d30c8f79-291b-4532-8f22-b693e61d6bb5"), "9a4e1d79-d64e-4ec4-85e5-53bdef5043f4");

            var flashcardsList = await mediator.Send(getFlashcardsListQ);

            Assert.Equal(2, flashcardsList.Count());
        }

        [Fact]
        public async Task TestB_CollectionNotExists()
        {
            var mocker = new MockDataV2();
            mocker.Reset();

            var serviceProvider = new ServiceCollection()
                .AddTransient<ICollectionRepo, CollectionRepo>()
                .AddTransient<IFlashcardRepo, FlashcardRepo>()
                .AddDbContext<AppDbContext>(options => options.UseSqlServer(MockDatabaseFactory.DbMockConnectionString))
                .AddMediatR(typeof(MediatREntryPoint).Assembly)
                .BuildServiceProvider();

            var mediator = serviceProvider.GetService<IMediator>();

            var getFlashcardsListQ = new GetFlashcardsListQ(Guid.Parse("00008f79-291b-4532-8f22-b693e61d6bb5"), "9a4e1d79-d64e-4ec4-85e5-53bdef5043f4");

            var flashcardsList = await mediator.Send(getFlashcardsListQ);

            Assert.Empty(flashcardsList);
        }

        [Fact]
        public async Task TestB_UserNotOwnCollection()
        {
            var mocker = new MockDataV2();
            mocker.Reset();

            var serviceProvider = new ServiceCollection()
                .AddTransient<ICollectionRepo, CollectionRepo>()
                .AddTransient<IFlashcardRepo, FlashcardRepo>()
                .AddDbContext<AppDbContext>(options => options.UseSqlServer(MockDatabaseFactory.DbMockConnectionString))
                .AddMediatR(typeof(MediatREntryPoint).Assembly)
                .BuildServiceProvider();

            var mediator = serviceProvider.GetService<IMediator>();

            var getFlashcardsListQ = new GetFlashcardsListQ(Guid.Parse("82c3a0d1-a73c-41e2-a8f3-ef525e5f0ffa"), "9a4e1d79-d64e-4ec4-85e5-53bdef5043f4");

            var flashcardsList = await mediator.Send(getFlashcardsListQ);

            Assert.Empty(flashcardsList);
        }
    }
}
