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
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace MobileService.Tests.Api.FlashcardTests
{
    [Collection("testdb_impact")]
    public class GetListTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public GetListTests(CustomWebApplicationFactory<Startup> factory)
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
            var mocker = new MockDataV5();
            mocker.Reset();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjlhNGUxZDc5LWQ2NGUtNGVjNC04NWU1LTUzYmRlZjUwNDNmNCIsIm5iZiI6MTYxOTc2OTYzOCwiZXhwIjoxNjE5ODU2MDM4LCJpc3MiOiJhIn0.tohmUFgbnXqaMoehSX9i-p_F6vpdoziu9Jz5XgM1N1k");

            var response = await _client.GetAsync("api/flashcard/getlistbycollectionid/d30c8f79-291b-4532-8f22-b693e61d6bb5");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var flashcards = JsonConvert.DeserializeObject<List<FlashcardGetModel>>(await response.Content.ReadAsStringAsync());

            Assert.Equal(2, flashcards.Count);

            Assert.NotNull(flashcards.FirstOrDefault(e => e.Id == Guid.Parse("6aa83ba0-1396-428f-adb7-d7ab972459eb")));
            Assert.NotNull(flashcards.FirstOrDefault(e => e.Id == Guid.Parse("30364c9b-e00e-4811-8921-69ab3db427cd")));
        }

        [Fact]
        public async Task TestB_UserNotOwnCollection()
        {
            var mocker = new MockDataV5();
            mocker.Reset();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjlhNGUxZDc5LWQ2NGUtNGVjNC04NWU1LTUzYmRlZjUwNDNmNCIsIm5iZiI6MTYxOTc2OTYzOCwiZXhwIjoxNjE5ODU2MDM4LCJpc3MiOiJhIn0.tohmUFgbnXqaMoehSX9i-p_F6vpdoziu9Jz5XgM1N1k");

            var response = await _client.GetAsync("api/flashcard/getlistbycollectionid/82c3a0d1-a73c-41e2-a8f3-ef525e5f0ffa");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var flashcards = JsonConvert.DeserializeObject<List<FlashcardGetModel>>(await response.Content.ReadAsStringAsync());

            Assert.Empty(flashcards);
        }
    }
}
