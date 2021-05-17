using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MobileService.Core;
using MobileService.Core.Queries.Practice;
using MobileService.DataAccess;
using MobileService.DataAccess.Repos;
using MobileService.Tests.MockData;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MobileService.Tests.Core.Queries.Practice
{
    [Collection("testdb_impact")]
    public class GetRandomExpiredFlashcardHTests
    {
        [Fact]
        public async Task TestA()
        {
            var mock = new MockDataV3();
            mock.Reset();

            var serviceProvider = new ServiceCollection()
                .AddTransient<IFlashcardProgressRepo, FlashcardProgressRepo>()
                .AddDbContext<AppDbContext>(options => options.UseSqlServer(MockDatabaseFactory.DbMockConnectionString))
                .AddMediatR(typeof(MediatREntryPoint).Assembly)
                .BuildServiceProvider();

            var mediator = serviceProvider.GetService<IMediator>();

            var getRandomExpiredFlashcardQ = new GetRandomExpiredFlashcardQ("9a4e1d79-d64e-4ec4-85e5-53bdef5043f4");

            var expectedReponses = new List<Guid>() { Guid.Parse("9884d783-427d-45d4-a1df-facaf81729f5"), Guid.Parse("2c083f4e-fdce-4c67-8ca7-e3c5d1b40d4e"), Guid.Parse("021fc2e4-e2cf-4120-a1af-df918ecad194") };
            var expectedReponsesNotContains = new List<Guid>() { Guid.Parse("594b1485-e842-482f-9b09-a649cb72bdb1"), Guid.Parse("91b5ae74-6197-449f-a4ef-c81068179822"), Guid.Parse("a6d821a0-75fd-4152-af8f-03fa17796430") };

            for (int i = 0; i < 100; i++)
            {
                var response = await mediator.Send(getRandomExpiredFlashcardQ);

                Assert.Contains(response.FlashcardProgressId, expectedReponses);
                Assert.DoesNotContain(response.FlashcardProgressId, expectedReponsesNotContains);
            }
        }
    }
}
