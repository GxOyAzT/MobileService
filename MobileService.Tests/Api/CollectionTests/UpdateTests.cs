using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MobileService.API;
using MobileService.DataAccess;
using MobileService.Entities.DataTransferModels.Collection;
using MobileService.Tests.MockData;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MobileService.Tests.Api.CollectionTests
{
    [Collection("testdb_impact")]
    public class UpdateTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public UpdateTests(CustomWebApplicationFactory<Startup> factory)
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
            var mocker = new MockDataV3();
            mocker.Reset();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjlhNGUxZDc5LWQ2NGUtNGVjNC04NWU1LTUzYmRlZjUwNDNmNCIsIm5iZiI6MTYxOTc2OTYzOCwiZXhwIjoxNjE5ODU2MDM4LCJpc3MiOiJhIn0.tohmUFgbnXqaMoehSX9i-p_F6vpdoziu9Jz5XgM1N1k");

            var inputDTO = new CollectionUpdateModel()
            {
                Id = Guid.Parse("d30c8f79-291b-4532-8f22-b693e61d6bb5"),
                Name = "name"
            };

            var response = await _client.PutAsync("api/collection/update", new StringContent(JsonConvert.SerializeObject(inputDTO), Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            using (var db = MockDatabaseFactory.Build())
            {
                Assert.Equal("name", db.Collections.FirstOrDefault(e => e.Id == Guid.Parse("d30c8f79-291b-4532-8f22-b693e61d6bb5")).Name);
            }
        }

        [Fact]
        public async Task TestB_IncorrectCollectionId()
        {
            var mocker = new MockDataV3();
            mocker.Reset();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjlhNGUxZDc5LWQ2NGUtNGVjNC04NWU1LTUzYmRlZjUwNDNmNCIsIm5iZiI6MTYxOTc2OTYzOCwiZXhwIjoxNjE5ODU2MDM4LCJpc3MiOiJhIn0.tohmUFgbnXqaMoehSX9i-p_F6vpdoziu9Jz5XgM1N1k");

            var inputDTO = new CollectionUpdateModel()
            {
                Id = Guid.Parse("00008f79-291b-4532-8f22-b693e61d6bb5"),
                Name = "name"
            };

            var response = await _client.PutAsync("api/collection/update", new StringContent(JsonConvert.SerializeObject(inputDTO), Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            using (var db = MockDatabaseFactory.Build())
            {
                Assert.Equal("User 1 Name 1", db.Collections.FirstOrDefault(e => e.Id == Guid.Parse("d30c8f79-291b-4532-8f22-b693e61d6bb5")).Name);
            }
        }

        [Fact]
        public async Task TestB_UserNotOwnCollection()
        {
            var mocker = new MockDataV3();
            mocker.Reset();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjlhNGUxZDc5LWQ2NGUtNGVjNC04NWU1LTUzYmRlZjUwNDNmNCIsIm5iZiI6MTYxOTc2OTYzOCwiZXhwIjoxNjE5ODU2MDM4LCJpc3MiOiJhIn0.tohmUFgbnXqaMoehSX9i-p_F6vpdoziu9Jz5XgM1N1k");

            var inputDTO = new CollectionUpdateModel()
            {
                Id = Guid.Parse("82c3a0d1-a73c-41e2-a8f3-ef525e5f0ffa"),
                Name = "name"
            };

            var response = await _client.PutAsync("api/collection/update", new StringContent(JsonConvert.SerializeObject(inputDTO), Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            using (var db = MockDatabaseFactory.Build())
            {
                Assert.Equal("User 2 Name 2", db.Collections.FirstOrDefault(e => e.Id == Guid.Parse("82c3a0d1-a73c-41e2-a8f3-ef525e5f0ffa")).Name);
            }
        }
    }
}
