using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MobileService.Core;
using MobileService.Core.Queries.Collections;
using MobileService.DataAccess;
using MobileService.DataAccess.Repos;
using MobileService.Tests.MockData;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MobileService.Tests.Core.Queries.Collections
{
    [Collection("testdb_impact")]
    public class GetCollectionByIdQTests
    {
        [Fact]
        public async Task TestA()
        {
            var mocker = new MockDataV2();
            mocker.Reset();

            var serviceProvider = new ServiceCollection()
                .AddTransient<ICollectionRepo, CollectionRepo>()
                .AddDbContext<AppDbContext>(options => options.UseSqlServer(MockDatabaseFactory.DbMockConnectionString))
                .AddMediatR(typeof(MediatREntryPoint).Assembly)
                .BuildServiceProvider();

            var mediator = serviceProvider.GetService<IMediator>();

            var getCollectionByIdQ = new GetCollectionByIdWithDailyStatsQ(Guid.Parse("d30c8f79-291b-4532-8f22-b693e61d6bb5"), "9a4e1d79-d64e-4ec4-85e5-53bdef5043f4");

            var answer = await mediator.Send(getCollectionByIdQ);

            Assert.NotNull(answer);
        }

        [Fact]
        public async Task TestB_CollectionNotExists()
        {
            var mocker = new MockDataV2();
            mocker.Reset();

            var serviceProvider = new ServiceCollection()
                .AddTransient<ICollectionRepo, CollectionRepo>()
                .AddDbContext<AppDbContext>(options => options.UseSqlServer(MockDatabaseFactory.DbMockConnectionString))
                .AddMediatR(typeof(MediatREntryPoint).Assembly)
                .BuildServiceProvider();

            var mediator = serviceProvider.GetService<IMediator>();

            var getCollectionByIdQ = new GetCollectionByIdWithDailyStatsQ(Guid.Parse("00008f79-291b-4532-8f22-b693e61d6bb5"), "9a4e1d79-d64e-4ec4-85e5-53bdef5043f4");

            var answer = await mediator.Send(getCollectionByIdQ);

            Assert.Null(answer);
        }

        [Fact]
        public async Task TestC_UserNotOwnCollection()
        {
            var mocker = new MockDataV2();
            mocker.Reset();

            var serviceProvider = new ServiceCollection()
                .AddTransient<ICollectionRepo, CollectionRepo>()
                .AddDbContext<AppDbContext>(options => options.UseSqlServer(MockDatabaseFactory.DbMockConnectionString))
                .AddMediatR(typeof(MediatREntryPoint).Assembly)
                .BuildServiceProvider();

            var mediator = serviceProvider.GetService<IMediator>();

            var getCollectionByIdQ = new GetCollectionByIdWithDailyStatsQ(Guid.Parse("82c3a0d1-a73c-41e2-a8f3-ef525e5f0ffa"), "9a4e1d79-d64e-4ec4-85e5-53bdef5043f4");

            var answer = await mediator.Send(getCollectionByIdQ);

            Assert.Null(answer);
        }
    }
}
