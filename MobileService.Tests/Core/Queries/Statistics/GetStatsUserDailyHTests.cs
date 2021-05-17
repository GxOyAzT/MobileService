using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MobileService.Core;
using MobileService.Core.Queries.StatsUser;
using MobileService.DataAccess;
using MobileService.DataAccess.Repos;
using MobileService.Tests.MockData;
using System.Threading.Tasks;
using Xunit;

namespace MobileService.Tests.Core.Queries.Statistics
{
    [Collection("testdb_impact")]
    public class GetStatsUserDailyHTests
    {
        [Fact]
        public async Task TestA()
        {
            var mock = new MockDataV5();
            mock.Reset();

            var serviceProvider = new ServiceCollection()
                .AddTransient<IFlashcardProgressRepo, FlashcardProgressRepo>()
                .AddDbContext<AppDbContext>(options => options.UseSqlServer(MockDatabaseFactory.DbMockConnectionString))
                .AddMediatR(typeof(MediatREntryPoint).Assembly)
                .BuildServiceProvider();

            var mediator = serviceProvider.GetService<IMediator>();

            var getStatsUserDailyQ = new GetStatsUserDailyQ("fcabcb46-12dc-4013-bc92-6f00aae903b4");

            var output = await mediator.Send(getStatsUserDailyQ);

            Assert.Equal(2, output.TotalFlashcards);
            Assert.Equal(1, output.ToLearnFlashcards);
            Assert.Equal(0, output.NewFlashcards);
        }
    }
}
