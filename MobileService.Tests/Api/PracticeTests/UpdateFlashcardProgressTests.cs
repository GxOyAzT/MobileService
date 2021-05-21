using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MobileService.API;
using MobileService.DataAccess;
using MobileService.Tests.MockData;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MobileService.Tests.Api.PracticeTests
{
    [Collection("testdb_impact")]
    public class UpdateFlashcardProgressTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public UpdateFlashcardProgressTests(CustomWebApplicationFactory<Startup> factory)
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


            var response = await _client.GetAsync("api/practice/updateflashcardprogress/594b1485-e842-482f-9b09-a649cb72bdb1/3");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            await Task.Delay(1000 * 5);

            using (var db = MockDatabaseFactory.Build())
            {
                var flashcardProgressModel = await db.FlashcardProgresses.FirstOrDefaultAsync(e => e.Id == Guid.Parse("594b1485-e842-482f-9b09-a649cb72bdb1"));

                Assert.Equal(0, flashcardProgressModel.CorrectInRow);
                Assert.Equal(DateTime.Now.Date, flashcardProgressModel.PracticeDate);
            }
        }

        [Fact]
        public async Task TestB()
        {
            var mocker = new MockDataV3();
            mocker.Reset();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjlhNGUxZDc5LWQ2NGUtNGVjNC04NWU1LTUzYmRlZjUwNDNmNCIsIm5iZiI6MTYxOTc2OTYzOCwiZXhwIjoxNjE5ODU2MDM4LCJpc3MiOiJhIn0.tohmUFgbnXqaMoehSX9i-p_F6vpdoziu9Jz5XgM1N1k");


            var response = await _client.GetAsync("api/practice/updateflashcardprogress/594b1485-e842-482f-9b09-a649cb72bdb1/2");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            await Task.Delay(1000 * 5);

            using (var db = MockDatabaseFactory.Build())
            {
                var flashcardProgressModel = await db.FlashcardProgresses.FirstOrDefaultAsync(e => e.Id == Guid.Parse("594b1485-e842-482f-9b09-a649cb72bdb1"));

                Assert.Equal(2, flashcardProgressModel.CorrectInRow);
                Assert.Equal(DateTime.Now.Date.AddDays(1), flashcardProgressModel.PracticeDate);
            }
        }

        [Fact]
        public async Task TestC()
        {
            var mocker = new MockDataV3();
            mocker.Reset();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjlhNGUxZDc5LWQ2NGUtNGVjNC04NWU1LTUzYmRlZjUwNDNmNCIsIm5iZiI6MTYxOTc2OTYzOCwiZXhwIjoxNjE5ODU2MDM4LCJpc3MiOiJhIn0.tohmUFgbnXqaMoehSX9i-p_F6vpdoziu9Jz5XgM1N1k");


            var response = await _client.GetAsync("api/practice/updateflashcardprogress/594b1485-e842-482f-9b09-a649cb72bdb1/1");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            await Task.Delay(1000 * 5);

            using (var db = MockDatabaseFactory.Build())
            {
                var flashcardProgressModel = await db.FlashcardProgresses.FirstOrDefaultAsync(e => e.Id == Guid.Parse("594b1485-e842-482f-9b09-a649cb72bdb1"));

                Assert.Equal(3, flashcardProgressModel.CorrectInRow);
                Assert.Equal(DateTime.Now.Date.AddDays(3), flashcardProgressModel.PracticeDate);
            }
        }

        [Fact]
        public async Task TestD()
        {
            var mocker = new MockDataV6();
            mocker.Reset();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjlhNGUxZDc5LWQ2NGUtNGVjNC04NWU1LTUzYmRlZjUwNDNmNCIsIm5iZiI6MTYxOTc2OTYzOCwiZXhwIjoxNjE5ODU2MDM4LCJpc3MiOiJhIn0.tohmUFgbnXqaMoehSX9i-p_F6vpdoziu9Jz5XgM1N1k");

            var response = await _client.GetAsync("api/practice/updateflashcardprogress/9dccbe2b-fd54-42e7-8f35-c0fba62855f7/3");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            await Task.Delay(1000 * 5);

            using (var db = MockDatabaseFactory.Build())
            {
                var flashcardProgressModel = await db.FlashcardProgresses.FirstOrDefaultAsync(e => e.Id == Guid.Parse("9dccbe2b-fd54-42e7-8f35-c0fba62855f7"));

                Assert.Equal(5, flashcardProgressModel.CorrectInRow);
                Assert.Equal(DateTime.Now.Date, flashcardProgressModel.PracticeDate);
            }
        }

        [Fact]
        public async Task TestE()
        {
            var mocker = new MockDataV6();
            mocker.Reset();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjlhNGUxZDc5LWQ2NGUtNGVjNC04NWU1LTUzYmRlZjUwNDNmNCIsIm5iZiI6MTYxOTc2OTYzOCwiZXhwIjoxNjE5ODU2MDM4LCJpc3MiOiJhIn0.tohmUFgbnXqaMoehSX9i-p_F6vpdoziu9Jz5XgM1N1k");


            var response = await _client.GetAsync("api/practice/updateflashcardprogress/9dccbe2b-fd54-42e7-8f35-c0fba62855f7/3");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var response2 = await _client.GetAsync("api/practice/updateflashcardprogress/9dccbe2b-fd54-42e7-8f35-c0fba62855f7/3");
            
            Assert.Equal(HttpStatusCode.OK, response2.StatusCode);

            await Task.Delay(1000 * 5);

            using (var db = MockDatabaseFactory.Build())
            {
                var flashcardProgressModel = await db.FlashcardProgresses.FirstOrDefaultAsync(e => e.Id == Guid.Parse("9dccbe2b-fd54-42e7-8f35-c0fba62855f7"));

                Assert.Equal(3, flashcardProgressModel.CorrectInRow);
                Assert.Equal(DateTime.Now.Date, flashcardProgressModel.PracticeDate);
            }
        }
    }
}
