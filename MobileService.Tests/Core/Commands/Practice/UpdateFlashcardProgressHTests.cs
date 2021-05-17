using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MobileService.Core;
using MobileService.Core.Commands.Practice;
using MobileService.DataAccess;
using MobileService.DataAccess.Repos;
using MobileService.Entities.Enums;
using MobileService.Tests.MockData;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MobileService.Tests.Core.Commands.Practice
{
    [Collection("testdb_impact")]
    public class UpdateFlashcardProgressHTests
    {
        [Fact]
        public async Task TestA()
        {
            var mock = new MockDataV3();
            mock.Reset();

            var serviceProvider = new ServiceCollection()
                .AddTransient<IFlashcardProgressRepo, FlashcardProgressRepo>()
                .AddTransient<IStatsUserRepo, StatsUserRepo>()
                .AddDbContext<AppDbContext>(options => options.UseSqlServer(MockDatabaseFactory.DbMockConnectionString))
                .AddMediatR(typeof(MediatREntryPoint).Assembly)
                .BuildServiceProvider();

            var mediator = serviceProvider.GetService<IMediator>();

            var updateFlashcardProgressC = new UpdateFlashcardProgressC(Guid.Parse("9884d783-427d-45d4-a1df-facaf81729f5"), FlashcardProgress.DontKnow, "9a4e1d79-d64e-4ec4-85e5-53bdef5043f4");

            var actionResponse = await mediator.Send(updateFlashcardProgressC);

            using (var db = MockDatabaseFactory.Build())
            {
                var flashcardProgressModel = await db.FlashcardProgresses.FirstOrDefaultAsync(e => e.Id == Guid.Parse("9884d783-427d-45d4-a1df-facaf81729f5"));

                Assert.Equal(0, flashcardProgressModel.CorrectInRow);
                Assert.Equal(DateTime.Now.Date, flashcardProgressModel.PracticeDate);
            }
        }

        [Fact]
        public async Task TestB()
        {
            var mock = new MockDataV3();
            mock.Reset();

            var serviceProvider = new ServiceCollection()
                .AddTransient<IFlashcardProgressRepo, FlashcardProgressRepo>()
                .AddTransient<IStatsUserRepo, StatsUserRepo>()
                .AddDbContext<AppDbContext>(options => options.UseSqlServer(MockDatabaseFactory.DbMockConnectionString))
                .AddMediatR(typeof(MediatREntryPoint).Assembly)
                .BuildServiceProvider();

            var mediator = serviceProvider.GetService<IMediator>();

            var updateFlashcardProgressC = new UpdateFlashcardProgressC(Guid.Parse("9884d783-427d-45d4-a1df-facaf81729f5"), FlashcardProgress.MediumKnow, "9a4e1d79-d64e-4ec4-85e5-53bdef5043f4");

            var actionResponse = await mediator.Send(updateFlashcardProgressC);

            using (var db = MockDatabaseFactory.Build())
            {
                var flashcardProgressModel = await db.FlashcardProgresses.FirstOrDefaultAsync(e => e.Id == Guid.Parse("9884d783-427d-45d4-a1df-facaf81729f5"));

                Assert.Equal(0, flashcardProgressModel.CorrectInRow);
                Assert.Equal(DateTime.Now.Date.AddDays(1), flashcardProgressModel.PracticeDate);
            }
        }

        [Fact]
        public async Task TestC()
        {
            var mock = new MockDataV3();
            mock.Reset();

            var serviceProvider = new ServiceCollection()
                .AddTransient<IFlashcardProgressRepo, FlashcardProgressRepo>()
                .AddTransient<IStatsUserRepo, StatsUserRepo>()
                .AddDbContext<AppDbContext>(options => options.UseSqlServer(MockDatabaseFactory.DbMockConnectionString))
                .AddMediatR(typeof(MediatREntryPoint).Assembly)
                .BuildServiceProvider();

            var mediator = serviceProvider.GetService<IMediator>();

            var updateFlashcardProgressC = new UpdateFlashcardProgressC(Guid.Parse("9884d783-427d-45d4-a1df-facaf81729f5"), FlashcardProgress.Know, "9a4e1d79-d64e-4ec4-85e5-53bdef5043f4");

            var actionResponse = await mediator.Send(updateFlashcardProgressC);

            using (var db = MockDatabaseFactory.Build())
            {
                var flashcardProgressModel = await db.FlashcardProgresses.FirstOrDefaultAsync(e => e.Id == Guid.Parse("9884d783-427d-45d4-a1df-facaf81729f5"));

                Assert.Equal(1, flashcardProgressModel.CorrectInRow);
                Assert.Equal(DateTime.Now.Date.AddDays(1), flashcardProgressModel.PracticeDate);
            }
        }
    }
}
