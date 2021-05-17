using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MobileService.Core;
using MobileService.Core.Commands.Flashcards;
using MobileService.DataAccess;
using MobileService.DataAccess.Repos;
using MobileService.Tests.MockData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MobileService.Tests.Core.Commands.Flashcrads
{
    [Collection("testdb_impact")]
    public class DeleteFlashcardTests
    {
        [Fact]
        public async Task TestA_OK()
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

            var deleteFlashcardC = new DeleteFlashcardC(Guid.Parse("6aa83ba0-1396-428f-adb7-d7ab972459eb"), "9a4e1d79-d64e-4ec4-85e5-53bdef5043f4");

            var actionResult = await mediator.Send(deleteFlashcardC);

            Assert.True(actionResult.IsSucceed);

            using (var db = MockDatabaseFactory.Build())
            {
                Assert.Single(db.Flashcards);
            }
        }

        [Fact]
        public async Task TestB_FlashcardNotExists()
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

            var deleteFlashcardC = new DeleteFlashcardC(Guid.Parse("00003ba0-1396-428f-adb7-d7ab972459eb"), "9a4e1d79-d64e-4ec4-85e5-53bdef5043f4");

            var actionResult = await mediator.Send(deleteFlashcardC);

            Assert.False(actionResult.IsSucceed);

            using (var db = MockDatabaseFactory.Build())
            {
                Assert.Equal(2, await db.Flashcards.CountAsync());
            }
        }

        [Fact]
        public async Task TestC_UserNotOwnFlashcard()
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

            var deleteFlashcardC = new DeleteFlashcardC(Guid.Parse("6aa83ba0-1396-428f-adb7-d7ab972459eb"), "fcabcb46-12dc-4013-bc92-6f00aae903b4");

            var actionResult = await mediator.Send(deleteFlashcardC);

            Assert.False(actionResult.IsSucceed);

            using (var db = MockDatabaseFactory.Build())
            {
                Assert.Equal(2, await db.Flashcards.CountAsync());
            }
        }
    }
}
