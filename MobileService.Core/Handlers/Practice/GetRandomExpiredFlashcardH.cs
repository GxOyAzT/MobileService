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
    public class GetRandomExpiredFlashcardH : IRequestHandler<GetRandomExpiredFlashcardQ, FlashcardPracticeGetModel>
    {
        private readonly IFlashcardProgressRepo _flashcardProgressRepo;

        public GetRandomExpiredFlashcardH(
            IFlashcardProgressRepo flashcardProgressRepo)
        {
            _flashcardProgressRepo = flashcardProgressRepo;
        }

        public async Task<FlashcardPracticeGetModel> Handle(GetRandomExpiredFlashcardQ request, CancellationToken cancellationToken)
        {
            var allUserFlashcards = await _flashcardProgressRepo.GetAllUserFlashcards(request.UserId);

            var expiredFlashcards = allUserFlashcards.Where(e => e.PracticeDate <= DateTime.Now.Date).ToList();
            
            if (!expiredFlashcards.Any())
            {
                return null;
            }

            var randomExpiredFlashcard = expiredFlashcards[(new Random()).Next(0, expiredFlashcards.Count())];

            return new FlashcardPracticeGetModel()
            {
                FlashcardProgressId = randomExpiredFlashcard.Id,
                Front = randomExpiredFlashcard.PracticeDirection == PracticeDirection.ForeignToNative ? randomExpiredFlashcard.FlashcardModel.Foreign : randomExpiredFlashcard.FlashcardModel.Native,
                Back = randomExpiredFlashcard.PracticeDirection == PracticeDirection.ForeignToNative ? randomExpiredFlashcard.FlashcardModel.Native : randomExpiredFlashcard.FlashcardModel.Foreign,
                PracticeDirection = randomExpiredFlashcard.PracticeDirection
            };
        }
    }
}
