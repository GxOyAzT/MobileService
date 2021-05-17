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
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MobileService.Tests.Api.PracticeTests
{
    [Collection("testdb_impact")]
    public class GetRandomExpiredForChooseTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public GetRandomExpiredForChooseTests(CustomWebApplicationFactory<Startup> factory)
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


            var response = await _client.GetAsync("api/practice/getrandomexpiredforchoose");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var retObj = JsonConvert.DeserializeObject<FlashcardPracticeChooseGetModel>(await response.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task TestB()
        {
            var mocker = new MockDataV5();
            mocker.Reset();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImZjYWJjYjQ2LTEyZGMtNDAxMy1iYzkyLTZmMDBhYWU5MDNiNCIsIm5iZiI6MTYyMTE1ODM5NCwiZXhwIjoxNjIxMjQ0Nzk0LCJpc3MiOiJhIn0.wrclVFPISY0O1Ll76i7rGrv0CvduGayE6RHhRfvm9TU");


            var response = await _client.GetAsync("api/practice/getrandomexpiredforchoose");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
