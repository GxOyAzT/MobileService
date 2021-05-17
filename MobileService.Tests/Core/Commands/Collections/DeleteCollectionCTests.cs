using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MobileService.Core;
using MobileService.Core.Commands.Collections;
using MobileService.DataAccess;
using MobileService.DataAccess.Repos;
using MobileService.Tests.MockData;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MobileService.Tests.Core.Commands.Collections
{
    [Collection("testdb_impact")]
    public class DeleteCollectionCTests
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

            var service = serviceProvider.GetService<IMediator>();

            var request = new DeleteCollectionC(Guid.Parse("d30c8f79-291b-4532-8f22-b693e61d6bb5"), "9a4e1d79-d64e-4ec4-85e5-53bdef5043f4");

            var actionResponse = await service.Send(request);

            Assert.True(actionResponse.IsSucceed);

            using (var db = MockDatabaseFactory.Build())
            {
                Assert.Equal(5, await db.Collections.CountAsync());
            }
        }

        [Fact]
        public async Task TestB_CollectionIdNotExists()
        {
            var mocker = new MockDataV2();
            mocker.Reset();

            var serviceProvider = new ServiceCollection()
                .AddTransient<ICollectionRepo, CollectionRepo>()
                .AddDbContext<AppDbContext>(options => options.UseSqlServer(MockDatabaseFactory.DbMockConnectionString))
                .AddMediatR(typeof(MediatREntryPoint).Assembly)
                .BuildServiceProvider();

            var service = serviceProvider.GetService<IMediator>();

            var request = new DeleteCollectionC(Guid.Parse("00008f79-291b-4532-8f22-b693e61d6bb5"), "9a4e1d79-d64e-4ec4-85e5-53bdef5043f4");

            var actionResponse = await service.Send(request);

            Assert.False(actionResponse.IsSucceed);

            using (var db = MockDatabaseFactory.Build())
            {
                Assert.Equal(6, await db.Collections.CountAsync());
            }
        }

        [Fact]
        public async Task TestB_UserNotOwnCollection()
        {
            var mocker = new MockDataV2();
            mocker.Reset();

            var serviceProvider = new ServiceCollection()
                .AddTransient<ICollectionRepo, CollectionRepo>()
                .AddDbContext<AppDbContext>(options => options.UseSqlServer(MockDatabaseFactory.DbMockConnectionString))
                .AddMediatR(typeof(MediatREntryPoint).Assembly)
                .BuildServiceProvider();

            var service = serviceProvider.GetService<IMediator>();

            var request = new DeleteCollectionC(Guid.Parse("82c3a0d1-a73c-41e2-a8f3-ef525e5f0ffa"), "9a4e1d79-d64e-4ec4-85e5-53bdef5043f4");

            var actionResponse = await service.Send(request);

            Assert.False(actionResponse.IsSucceed);

            using (var db = MockDatabaseFactory.Build())
            {
                Assert.Equal(6, await db.Collections.CountAsync());
            }
        }
    }
}
