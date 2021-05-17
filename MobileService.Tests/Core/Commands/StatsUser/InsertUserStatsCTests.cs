using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MobileService.Core;
using MobileService.Core.Commands.StatsUser;
using MobileService.DataAccess;
using MobileService.DataAccess.Repos;
using MobileService.Tests.MockData;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MobileService.Tests.Core.Commands.StatsUser
{
    [Collection("testdb_impact")]
    public class InsertUserStatsCTests
    {
        [Fact]
        public async Task TestA()
        {
            var mock = new MockDataV4();
            mock.Reset();

            var serviceProvider = new ServiceCollection()
                .AddTransient<IStatsUserRepo, StatsUserRepo>()
                .AddDbContext<AppDbContext>(options => options.UseSqlServer(MockDatabaseFactory.DbMockConnectionString))
                .AddMediatR(typeof(MediatREntryPoint).Assembly)
                .BuildServiceProvider();

            var mediator = serviceProvider.GetService<IMediator>();

            var command = new IncrementStatsUserC("9a4e1d79-d64e-4ec4-85e5-53bdef5043f4");

            var actionResponse = await mediator.Send(command);

            Assert.True(actionResponse.IsSucceed);

            using (var db = MockDatabaseFactory.Build())
            {
                Assert.Equal(3, await db.StatsUserModels.CountAsync());
                Assert.Equal(4, db.StatsUserModels.FirstOrDefault(e => e.Id == Guid.Parse("d28e4728-1082-477d-b9c8-be81aa165efb")).FlashcardsTurnOverCount);
                Assert.Equal(2, db.StatsUserModels.FirstOrDefault(e => e.Id == Guid.Parse("1d43f5dd-ef30-45e1-a99d-d3183807b953")).FlashcardsTurnOverCount);
            }
        }

        [Fact]
        public async Task TestB()
        {
            var mock = new MockDataV4();
            mock.Reset();

            var serviceProvider = new ServiceCollection()
                .AddTransient<IStatsUserRepo, StatsUserRepo>()
                .AddDbContext<AppDbContext>(options => options.UseSqlServer(MockDatabaseFactory.DbMockConnectionString))
                .AddMediatR(typeof(MediatREntryPoint).Assembly)
                .BuildServiceProvider();

            var mediator = serviceProvider.GetService<IMediator>();

            var command = new IncrementStatsUserC("a071553b-70e4-4998-aac2-37883d2d83ab");

            var actionResponse = await mediator.Send(command);

            Assert.True(actionResponse.IsSucceed);

            using (var db = MockDatabaseFactory.Build())
            {
                Assert.Equal(4, await db.StatsUserModels.CountAsync());
                Assert.Equal(3, db.StatsUserModels.FirstOrDefault(e => e.Id == Guid.Parse("d28e4728-1082-477d-b9c8-be81aa165efb")).FlashcardsTurnOverCount);
                Assert.Equal(2, db.StatsUserModels.FirstOrDefault(e => e.Id == Guid.Parse("1d43f5dd-ef30-45e1-a99d-d3183807b953")).FlashcardsTurnOverCount);
                Assert.Equal(1, db.StatsUserModels.FirstOrDefault(e => e.UserId == "a071553b-70e4-4998-aac2-37883d2d83ab" && e.Day == DateTime.Now.Date).FlashcardsTurnOverCount);
            }
        }
    }
}
