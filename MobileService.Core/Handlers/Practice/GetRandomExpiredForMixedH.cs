using MediatR;
using MobileService.Core.Queries.Practice;
using MobileService.DataAccess.Repos;
using MobileService.Entities.DataTransferModels.Flashcard;
using System.Threading;
using System.Threading.Tasks;

namespace MobileService.Core.Handlers.Practice
{
    public class GetRandomExpiredForMixedH : IRequestHandler<GetRandomExpiredForMixedQ, FlashcardPracticeMixedGetModel>
    {
        private readonly IMediator _mediator;
        private readonly IFlashcardProgressRepo _flashcardProgressRepo;

        public GetRandomExpiredForMixedH(
            IMediator mediator,
            IFlashcardProgressRepo flashcardProgressRepo)
        {
            _mediator = mediator;
            _flashcardProgressRepo = flashcardProgressRepo;
        }

        public async Task<FlashcardPracticeMixedGetModel> Handle(GetRandomExpiredForMixedQ request, CancellationToken cancellationToken)
        {
            var choosenFlashcard = await _mediator.Send(new GetRandomExpiredForChooseQ(request.UserId));

            if (choosenFlashcard == null)
            {
                return null;
            }

            var choosenFlashcardDb = await _flashcardProgressRepo.Get(choosenFlashcard.FlashcardProgressId);

            return new FlashcardPracticeMixedGetModel()
            {
                FlashcardProgressId = choosenFlashcard.FlashcardProgressId,
                Front = choosenFlashcard.Front,
                AnsA = choosenFlashcard.AnsA,
                AnsB = choosenFlashcard.AnsB,
                AnsC = choosenFlashcard.AnsC,
                CorrectAns = choosenFlashcard.CorrectAns,
                Back = choosenFlashcard.CorrectAns == 1 ? choosenFlashcard.AnsA : choosenFlashcard.CorrectAns == 2 ? choosenFlashcard.AnsB : choosenFlashcard.AnsC,
                CorrectAnsInRow = choosenFlashcardDb.CorrectInRow,
                PracticeDirection = choosenFlashcardDb.PracticeDirection
            };
        }
    }
}
