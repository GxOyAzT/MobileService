using MobileService.Core.Builders.Entities.Flashcard;
using MobileService.Core.Builders.Entities.Flashcard.ConcreteBuilders;
using MobileService.Entities.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MobileService.Tests.Core.Builders.Entities
{
    public class FlashcardBuilderTests
    {
        [Fact]
        public void TestA_BuilderNotPassed()
        {
            FlashcardBuilderDirectorRequest flashcardBuilderDirectorRequest = new FlashcardBuilderDirectorRequest(
                "native",
                "foreign",
                Guid.Parse("caacdd63-8c99-4f9b-94ea-3f68c8438a7d"),
                null);

            FlashcardBuilderDirector testService = new FlashcardBuilderDirector();

            Assert.ThrowsAsync<BuilderNotDefinedException>(() => testService.Handle(flashcardBuilderDirectorRequest, new System.Threading.CancellationToken()));
        }

        [Fact]
        public async Task TestA_Ok_TestForAdvanced()
        {
            FlashcardBuilderDirectorRequest flashcardBuilderDirectorRequest = new FlashcardBuilderDirectorRequest(
                "native",
                "foreign",
                Guid.Parse("caacdd63-8c99-4f9b-94ea-3f68c8438a7d"),
                new FlashcardBuilderAdvancedProgress());

            FlashcardBuilderDirector testService = new FlashcardBuilderDirector();

            var output = await testService.Handle(flashcardBuilderDirectorRequest, new System.Threading.CancellationToken());

            Assert.Equal("native", output.Native);
            Assert.Equal("foreign", output.Foreign);

            Assert.Equal(DateTime.Now.Date, output.FlashcardProgressModels.FirstOrDefault().PracticeDate);
            Assert.Equal(8, output.FlashcardProgressModels.FirstOrDefault().CorrectInRow);
        }
    }
}
