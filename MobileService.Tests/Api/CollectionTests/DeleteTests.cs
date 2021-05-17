using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MobileService.API;
using MobileService.DataAccess;
using MobileService.Tests.MockData;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace MobileService.Tests.Api.CollectionTests
{
    [Collection("testdb_impact")]
    public class DeleteTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public DeleteTests(CustomWebApplicationFactory<Startup> factory)
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
            var mocker = new MockDataV2();
            mocker.Reset();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjlhNGUxZDc5LWQ2NGUtNGVjNC04NWU1LTUzYmRlZjUwNDNmNCIsIm5iZiI6MTYxOTc2OTYzOCwiZXhwIjoxNjE5ODU2MDM4LCJpc3MiOiJhIn0.tohmUFgbnXqaMoehSX9i-p_F6vpdoziu9Jz5XgM1N1k");

            var response = await _client.DeleteAsync("api/collection/delete/d30c8f79-291b-4532-8f22-b693e61d6bb5");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            using (var db = MockDatabaseFactory.Build())
            {
                Assert.Equal(5, await db.Collections.CountAsync());
            }
        }

        [Fact]
        public async Task TestB_CollectionOfIdNotExists()
        {
            var mocker = new MockDataV2();
            mocker.Reset();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjlhNGUxZDc5LWQ2NGUtNGVjNC04NWU1LTUzYmRlZjUwNDNmNCIsIm5iZiI6MTYxOTc2OTYzOCwiZXhwIjoxNjE5ODU2MDM4LCJpc3MiOiJhIn0.tohmUFgbnXqaMoehSX9i-p_F6vpdoziu9Jz5XgM1N1k");

            var response = await _client.DeleteAsync("api/collection/delete/00008f79-291b-4532-8f22-b693e61d6bb5");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            using (var db = MockDatabaseFactory.Build())
            {
                Assert.Equal(6, await db.Collections.CountAsync());
            }
        }

        [Fact]
        public async Task TestC_UserNotOwnCollection()
        {
            var mocker = new MockDataV2();
            mocker.Reset();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjlhNGUxZDc5LWQ2NGUtNGVjNC04NWU1LTUzYmRlZjUwNDNmNCIsIm5iZiI6MTYxOTc2OTYzOCwiZXhwIjoxNjE5ODU2MDM4LCJpc3MiOiJhIn0.tohmUFgbnXqaMoehSX9i-p_F6vpdoziu9Jz5XgM1N1k");

            var response = await _client.DeleteAsync("api/collection/delete/82c3a0d1-a73c-41e2-a8f3-ef525e5f0ffa");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            using (var db = MockDatabaseFactory.Build())
            {
                Assert.Equal(6, await db.Collections.CountAsync());
            }

        }
    }
}