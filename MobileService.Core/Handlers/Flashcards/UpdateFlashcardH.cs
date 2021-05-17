using MediatR;
using MobileService.Core.Commands.Flashcards;
using MobileService.Core.Queries.Collections;
using MobileService.Core.Queries.Flashcards;
using MobileService.DataAccess.Repos;
using MobileService.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MobileService.Core.Handlers.Flashcards
{
    public class UpdateFlashcardH : IRequestHandler<UpdateFlashcardC, ActionReponseModel>
    {
        private readonly IFlashcardRepo _flashcardRepo;
        private readonly IMediator _mediator;

        public UpdateFlashcardH(
            IFlashcardRepo flashcardRepo,
            IMediator mediator)
        {
            _flashcardRepo = flashcardRepo;
            _mediator = mediator;
        }

        public async Task<ActionReponseModel> Handle(UpdateFlashcardC request, CancellationToken cancellationToken)
        {
            if (request.Flashcard == null)
            {
                return new ActionReponseModel(false, "Incorrect input format");
            }

            if (string.IsNullOrEmpty(request.Flashcard.Foreign) || string.IsNullOrEmpty(request.Flashcard.Native))
            {
                return new ActionReponseModel(false, "Foreign and native cannot be empty value.");
            }

            var flashcardDb = await _flashcardRepo.Get(request.Flashcard.Id);

            if (flashcardDb == null)
            {
                return new ActionReponseModel(false, "Cannot find flashcard");
            }

            var getCollectionById = new GetCollectionByIdWithDailyStatsQ(flashcardDb.CollectionModelId, request.UserId);

            if (await _mediator.Send(getCollectionById) == null)
            {
                return new ActionReponseModel(false, "User do not own this flashcard");
            }

            flashcardDb.Native = request.Flashcard.Native;
            flashcardDb.Foreign = request.Flashcard.Foreign;

            await _flashcardRepo.Update(flashcardDb);

            return new ActionReponseModel(true);
        }
    }
}
