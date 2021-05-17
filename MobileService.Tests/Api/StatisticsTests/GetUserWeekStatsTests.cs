using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MobileService.API;
using MobileService.DataAccess;
using MobileService.Entities.DataTransferModels.Statistics;
using MobileService.Tests.MockData;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace MobileService.Tests.Api.StatisticsTests
{
    [Collection("testdb_impact")]
    public class GetUserWeekStatsTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public GetUserWeekStatsTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddDbContext<AppDbContext>(options =>
                    {
                        options.UseSqlServer(MockDatabaseFactory.DbMockConnectionString);
                    });
                });
            }).CreateClient(new WebApplicationFactoryClientOptions());
        }

        [Fact]
        public async Task TestA()
        {
            var mock = new MockDataV4();
            mock.Reset();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjlhNGUxZDc5LWQ2NGUtNGVjNC04NWU1LTUzYmRlZjUwNDNmNCIsIm5iZiI6MTYxOTc2OTYzOCwiZXhwIjoxNjE5ODU2MDM4LCJpc3MiOiJhIn0.tohmUFgbnXqaMoehSX9i-p_F6vpdoziu9Jz5XgM1N1k");

            var response = await _client.GetAsync("api/statistics/getuserweekstats");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseModel = JsonConvert.DeserializeObject<StatsUserTurnOverWeekGetModel>(await response.Content.ReadAsStringAsync());

            Assert.Equal(3, responseModel.TodayCount);
            Assert.Equal(DateTime.Now.Date.DayOfWeek.ToString().Substring(0,3), responseModel.TodayDate);

            Assert.Equal(2, responseModel.YesterdayCount);

            Assert.Equal(0, responseModel.ThreeDayBeforeCount);

            Assert.Equal(0, responseModel.FourDayBeforeCount);

            Assert.Equal(0, responseModel.SevenDayBeforeCount);
            Assert.Equal(DateTime.Now.Date.AddDays(-6).DayOfWeek.ToString().Substring(0, 3), responseModel.SevenDayBeforeDate);
        }
    }
}
