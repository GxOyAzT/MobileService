using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MobileService.Core;
using MobileService.Core.Commands.Flashcards;
using MobileService.DataAccess;
using MobileService.DataAccess.Repos;
using MobileService.Entities.DataTransferModels.Flashcard;
using MobileService.Tests.MockData;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MobileService.Tests.Core.Commands.Flashcrads
{
    [Collection("testdb_impact")]
    public class InsertFlashcardCTests
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

            var service = serviceProvider.GetService<IMediator>();

            var inputModel = new FlashcardInsertModel()
            {
                CollectionId = Guid.Parse("d30c8f79-291b-4532-8f22-b693e61d6bb5"),
                Foreign = "Foreign ok",
                Native = "Native ok"
            };

            var insertFlashcardC = new InsertFlashcardC(inputModel, "9a4e1d79-d64e-4ec4-85e5-53bdef5043f4");

            var actionResult = await service.Send(insertFlashcardC, new System.Threading.CancellationToken());

            Assert.True(actionResult.IsSucceed);

            using (var db = MockDatabaseFactory.Build())
            {
                Assert.Equal(3, await db.Flashcards.CountAsync());
                Assert.Equal(6, await db.FlashcardProgresses.CountAsync());
            }
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

            var service = serviceProvider.GetService<IMediator>();

            var inputModel = new FlashcardInsertModel()
            {
                CollectionId = Guid.Parse("00008f79-291b-4532-8f22-b693e61d6bb5"),
                Foreign = "Foreign ok",
                Native = "Native ok"
            };

            var insertFlashcardC = new InsertFlashcardC(inputModel, "9a4e1d79-d64e-4ec4-85e5-53bdef5043f4");

            var actionResult = await service.Send(insertFlashcardC, new System.Threading.CancellationToken());

            Assert.False(actionResult.IsSucceed);

            using (var db = MockDatabaseFactory.Build())
            {
                Assert.Equal(2, await db.Flashcards.CountAsync());
                Assert.Equal(4, await db.FlashcardProgresses.CountAsync());
            }
        }

        [Fact]
        public async Task TestC_UserNotOwnCollection()
        {
            var mocker = new MockDataV2();
            mocker.Reset();

            var serviceProvider = new ServiceCollection()
                .AddTransient<ICollectionRepo, CollectionRepo>()
                .AddTransient<IFlashcardRepo, FlashcardRepo>()
                .AddDbContext<AppDbContext>(options => options.UseSqlServer(MockDatabaseFactory.DbMockConnectionString))
                .AddMediatR(typeof(MediatREntryPoint).Assembly)
                .BuildServiceProvider();

            var service = serviceProvider.GetService<IMediator>();

            var inputModel = new FlashcardInsertModel()
            {
                CollectionId = Guid.Parse("82c3a0d1-a73c-41e2-a8f3-ef525e5f0ffa"),
                Foreign = "Foreign ok",
                Native = "Native ok"
            };

            var insertFlashcardC = new InsertFlashcardC(inputModel, "9a4e1d79-d64e-4ec4-85e5-53bdef5043f4");

            var actionResult = await service.Send(insertFlashcardC, new System.Threading.CancellationToken());

            Assert.False(actionResult.IsSucceed);

            using (var db = MockDatabaseFactory.Build())
            {
                Assert.Equal(2, await db.Flashcards.CountAsync());
                Assert.Equal(4, await db.FlashcardProgresses.CountAsync());
            }
        }

        [Fact]
        public async Task TestC_EmptyForeignProp()
        {
            var mocker = new MockDataV2();
            mocker.Reset();

            var serviceProvider = new ServiceCollection()
                .AddTransient<ICollectionRepo, CollectionRepo>()
                .AddTransient<IFlashcardRepo, FlashcardRepo>()
                .AddDbContext<AppDbContext>(options => options.UseSqlServer(MockDatabaseFactory.DbMockConnectionString))
                .AddMediatR(typeof(MediatREntryPoint).Assembly)
                .BuildServiceProvider();

            var service = serviceProvider.GetService<IMediator>();

            var inputModel = new FlashcardInsertModel()
            {
                CollectionId = Guid.Parse("d30c8f79-291b-4532-8f22-b693e61d6bb5"),
                Foreign = "",
                Native = "Native ok"
            };

            var insertFlashcardC = new InsertFlashcardC(inputModel, "9a4e1d79-d64e-4ec4-85e5-53bdef5043f4");

            var actionResult = await service.Send(insertFlashcardC, new System.Threading.CancellationToken());

            Assert.False(actionResult.IsSucceed);

            using (var db = MockDatabaseFactory.Build())
            {
                Assert.Equal(2, await db.Flashcards.CountAsync());
                Assert.Equal(4, await db.FlashcardProgresses.CountAsync());
            }
        }

        [Fact]
        public async Task TestC_EmptyNativeProp()
        {
            var mocker = new MockDataV2();
            mocker.Reset();

            var serviceProvider = new ServiceCollection()
                .AddTransient<ICollectionRepo, CollectionRepo>()
                .AddTransient<IFlashcardRepo, FlashcardRepo>()
                .AddDbContext<AppDbContext>(options => options.UseSqlServer(MockDatabaseFactory.DbMockConnectionString))
                .AddMediatR(typeof(MediatREntryPoint).Assembly)
                .BuildServiceProvider();

            var service = serviceProvider.GetService<IMediator>();

            var inputModel = new FlashcardInsertModel()
            {
                CollectionId = Guid.Parse("d30c8f79-291b-4532-8f22-b693e61d6bb5"),
                Foreign = "Foreign ok",
                Native = ""
            };

            var insertFlashcardC = new InsertFlashcardC(inputModel, "9a4e1d79-d64e-4ec4-85e5-53bdef5043f4");

            var actionResult = await service.Send(insertFlashcardC, new System.Threading.CancellationToken());

            Assert.False(actionResult.IsSucceed);

            using (var db = MockDatabaseFactory.Build())
            {
                Assert.Equal(2, await db.Flashcards.CountAsync());
                Assert.Equal(4, await db.FlashcardProgresses.CountAsync());
            }
        }
    }
}
