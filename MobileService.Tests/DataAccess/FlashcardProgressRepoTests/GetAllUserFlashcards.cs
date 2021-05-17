using MobileService.DataAccess.Repos;
using MobileService.Tests.MockData;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MobileService.Tests.DataAccess.FlashcardProgressRepoTests
{
    [Collection("testdb_impact")]
    public class GetAllUserFlashcards
    {
        [Fact]
        public async Task TestA()
        {
            var mocker = new MockDataV3();
            mocker.Reset();

            var flashcardProgressRepo = BuildUnitForTests();

            var output = await flashcardProgressRepo.GetAllUserFlashcards("9a4e1d79-d64e-4ec4-85e5-53bdef5043f4");

            Assert.Equal(6, output.Count());
        }

        [Fact]
        public async Task TestB()
        {
            var mocker = new MockDataV3();
            mocker.Reset();

            var flashcardProgressRepo = BuildUnitForTests();

            var output = await flashcardProgressRepo.GetAllUserFlashcards("fcabcb46-12dc-4013-bc92-6f00aae903b4");

            Assert.Empty(output);
        }

        private IFlashcardProgressRepo BuildUnitForTests() =>
            new FlashcardProgressRepo(MockDatabaseFactory.Build());
    }
}
