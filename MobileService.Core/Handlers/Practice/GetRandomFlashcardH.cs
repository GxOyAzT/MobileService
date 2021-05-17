using MediatR;
using MobileService.Core.Queries.Practice;
using MobileService.DataAccess.Repos;
using MobileService.Entities.DataTransferModels.Flashcard;
using MobileService.Entities.Enums;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MobileService.Core.Handlers.Practice
{
    public class GetRandomFlashcardH : IRequestHandler<GetRandomFlashcardQ, FlashcardPracticeGetModel>
    {
        private readonly IFlashcardProgressRepo _flashcardProgressRepo;

        public GetRandomFlashcardH(IFlashcardProgressRepo flashcardProgressRepo)
        {
            _flashcardProgressRepo = flashcardProgressRepo;
        }
        
        public async Task<FlashcardPracticeGetModel> Handle(GetRandomFlashcardQ request, CancellationToken cancellationToken)
        {
            var flashcards = await _flashcardProgressRepo.GetAllUserFlashcards(request.UserId);

            var randomFlashcard = flashcards[(new Random()).Next(0, flashcards.Count())];

            return new FlashcardPracticeGetModel()
            {
                FlashcardProgressId = randomFlashcard.Id,
                Front = randomFlashcard.PracticeDirection == PracticeDirection.ForeignToNative ? randomFlashcard.FlashcardModel.Foreign : randomFlashcard.FlashcardModel.Native,
                Back = randomFlashcard.PracticeDirection == PracticeDirection.ForeignToNative ? randomFlashcard.FlashcardModel.Native : randomFlashcard.FlashcardModel.Foreign
            };
        }
    }
}
