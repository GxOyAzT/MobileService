using MobileService.Core.Handlers;
using MobileService.Core.Queries;
using MobileService.DataAccess.Repos;
using MobileService.Tests.MockData;
using System;
using System.Linq;
using System.Threading;
using Xunit;

namespace MobileService.Tests.Core.Queries.Collections
{
    [Collection("testdb_impact")]
    public class GetCollectionsListQTests
    {
        [Fact]
        public async void TestA()
        {
            var mocker = new MockDataV2();
            mocker.Reset();

            var _handler = BuildHandlerForTests();

            var output = await _handler.Handle(new GetCollectionsListQ("9a4e1d79-d64e-4ec4-85e5-53bdef5043f4"), new CancellationToken());

            Assert.Equal(2, output.Count());

            var collection = output.FirstOrDefault(e => e.Id == Guid.Parse("d30c8f79-291b-4532-8f22-b693e61d6bb5"));

            Assert.Equal("User 1 Name 1", collection.Name);

            //Assert.Equal(4, collection.TotalFlashcards);
            //Assert.Equal(1, collection.ToLearnFlashcards);
            //Assert.Equal(0, collection.NewFlashcards);
        }

        private GetCollectionsListH BuildHandlerForTests() =>
            new GetCollectionsListH(
                new CollectionRepo(MockDatabaseFactory.Build()));
    }
}
