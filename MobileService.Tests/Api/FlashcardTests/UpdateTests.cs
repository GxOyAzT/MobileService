using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MobileService.API;
using MobileService.DataAccess;
using MobileService.Entities.DataTransferModels.Flashcard;
using MobileService.Tests.MockData;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MobileService.Tests.Api.FlashcardTests
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

            var insertDTO = new FlashcardUpdateModel()
            {
                Id = Guid.Parse("6aa83ba0-1396-428f-adb7-d7ab972459eb"),
                Foreign = "foreign new",
                Native = "native new"
            };

            var response = await _client.PutAsync("api/flashcard/update", new StringContent(JsonConvert.SerializeObject(insertDTO), Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            using (var db = MockDatabaseFactory.Build())
            {
                Assert.Equal("foreign new", (await db.Flashcards.FirstOrDefaultAsync(e => e.Id == Guid.Parse("6aa83ba0-1396-428f-adb7-d7ab972459eb"))).Foreign);
                Assert.Equal("native new", (await db.Flashcards.FirstOrDefaultAsync(e => e.Id == Guid.Parse("6aa83ba0-1396-428f-adb7-d7ab972459eb"))).Native);
            }
        }

        [Fact]
        public async Task TestB_FlashcardOfIdNotExists()
        {
            var mocker = new MockDataV3();
            mocker.Reset();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjlhNGUxZDc5LWQ2NGUtNGVjNC04NWU1LTUzYmRlZjUwNDNmNCIsIm5iZiI6MTYxOTc2OTYzOCwiZXhwIjoxNjE5ODU2MDM4LCJpc3MiOiJhIn0.tohmUFgbnXqaMoehSX9i-p_F6vpdoziu9Jz5XgM1N1k");

            var insertDTO = new FlashcardUpdateModel()
            {
                Id = Guid.Parse("00003ba0-1396-428f-adb7-d7ab972459eb"),
                Foreign = "foreign new",
                Native = "native new"
            };

            var response = await _client.PutAsync("api/flashcard/update", new StringContent(JsonConvert.SerializeObject(insertDTO), Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            using (var db = MockDatabaseFactory.Build())
            {
                Assert.Equal("Foreign 1", (await db.Flashcards.FirstOrDefaultAsync(e => e.Id == Guid.Parse("6aa83ba0-1396-428f-adb7-d7ab972459eb"))).Foreign);
                Assert.Equal("Native 1", (await db.Flashcards.FirstOrDefaultAsync(e => e.Id == Guid.Parse("6aa83ba0-1396-428f-adb7-d7ab972459eb"))).Native);
            }
        }

        [Fact]
        public async Task TestC_UserDoNotOwnFlashcard()
        {
            var mocker = new MockDataV5();
            mocker.Reset();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjlhNGUxZDc5LWQ2NGUtNGVjNC04NWU1LTUzYmRlZjUwNDNmNCIsIm5iZiI6MTYxOTc2OTYzOCwiZXhwIjoxNjE5ODU2MDM4LCJpc3MiOiJhIn0.tohmUFgbnXqaMoehSX9i-p_F6vpdoziu9Jz5XgM1N1k");

            var insertDTO = new FlashcardUpdateModel()
            {
                Id = Guid.Parse("691de3f1-8117-465f-b8d9-7cfcefc372fe"),
                Foreign = "foreign new",
                Native = "native new"
            };

            var response = await _client.PutAsync("api/flashcard/update", new StringContent(JsonConvert.SerializeObject(insertDTO), Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            using (var db = MockDatabaseFactory.Build())
            {
                Assert.Equal("Foreign 4", (await db.Flashcards.FirstOrDefaultAsync(e => e.Id == Guid.Parse("691de3f1-8117-465f-b8d9-7cfcefc372fe"))).Foreign);
                Assert.Equal("Native 4", (await db.Flashcards.FirstOrDefaultAsync(e => e.Id == Guid.Parse("691de3f1-8117-465f-b8d9-7cfcefc372fe"))).Native);
            }
        }
    }
}
