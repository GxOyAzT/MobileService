using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MobileService.Core;
using MobileService.Core.Commands.Collections;
using MobileService.DataAccess;
using MobileService.DataAccess.Repos;
using MobileService.Entities;
using MobileService.Entities.DataTransferModels.Collection;
using MobileService.Tests.MockData;
using System.Threading.Tasks;
using Xunit;

namespace MobileService.Tests.Core.Commands.Collections
{
    [Collection("testdb_impact")]
    public class InsertCollectionCTests
    {
        [Fact]
        public async Task TestA()
        {
            var mocker = new MockDataV2();
            mocker.Reset();

            var serviceProvider = new ServiceCollection()
                .AddTransient<ICollectionRepo, CollectionRepo>()
                .AddAutoMapper(typeof(MappingProfile))
                .AddDbContext<AppDbContext>(options => options.UseSqlServer(MockDatabaseFactory.DbMockConnectionString))
                .AddMediatR(typeof(MediatREntryPoint).Assembly)
                .BuildServiceProvider();

            var service = serviceProvider.GetService<IMediator>();

            await service.Send(new InsertCollectionC(new CollectionInsertModel() { Name = "Correct Name" }, "9a4e1d79-d64e-4ec4-85e5-53bdef5043f4"));

            using (var db = MockDatabaseFactory.Build())
            {
                Assert.Equal(7, await db.Collections.CountAsync());
            }
        }

        [Fact]
        public async Task TestB_NameAlreadyExists()
        {
            var mocker = new MockDataV2();
            mocker.Reset();

            var serviceProvider = new ServiceCollection()
                .AddTransient<ICollectionRepo, CollectionRepo>()
                .AddAutoMapper(typeof(MappingProfile))
                .AddDbContext<AppDbContext>(options => options.UseSqlServer(MockDatabaseFactory.DbMockConnectionString))
                .AddMediatR(typeof(MediatREntryPoint).Assembly)
                .BuildServiceProvider();

            var service = serviceProvider.GetService<IMediator>();

            var result = await service.Send(new InsertCollectionC(new CollectionInsertModel() { Name = "User 1 Name 1" }, "9a4e1d79-d64e-4ec4-85e5-53bdef5043f4"));

            Assert.False(result.IsSucceed);

            using (var db = MockDatabaseFactory.Build())
            {
                Assert.Equal(6, await db.Collections.CountAsync());
            }
        }

        [Fact]
        public async Task TestB_NoNamePassed()
        {
            var mocker = new MockDataV2();
            mocker.Reset();

            var serviceProvider = new ServiceCollection()
                .AddTransient<ICollectionRepo, CollectionRepo>()
                .AddAutoMapper(typeof(MappingProfile))
                .AddDbContext<AppDbContext>(options => options.UseSqlServer(MockDatabaseFactory.DbMockConnectionString))
                .AddMediatR(typeof(MediatREntryPoint).Assembly)
                .BuildServiceProvider();

            var service = serviceProvider.GetService<IMediator>();

            var result = await service.Send(new InsertCollectionC(new CollectionInsertModel() { Name = "" }, "9a4e1d79-d64e-4ec4-85e5-53bdef5043f4"));

            Assert.False(result.IsSucceed);

            using (var db = MockDatabaseFactory.Build())
            {
                Assert.Equal(6, await db.Collections.CountAsync());
            }
        }
    }
}
