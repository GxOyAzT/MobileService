using MediatR;
using MobileService.Core.Commands.Flashcards;
using MobileService.Core.Queries.Collections;
using MobileService.DataAccess.Repos;
using MobileService.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MobileService.Core.Handlers.Flashcards
{
    public class DeleteFlashcardH : IRequestHandler<DeleteFlashcardC, ActionReponseModel>
    {
        private readonly IFlashcardRepo _flashcardRepo;
        private readonly IMediator _mediator;

        public DeleteFlashcardH(
            IFlashcardRepo flashcardRepo,
            IMediator mediator)
        {
            _flashcardRepo = flashcardRepo;
            _mediator = mediator;
        }

        public async Task<ActionReponseModel> Handle(DeleteFlashcardC request, CancellationToken cancellationToken)
        {
            var flashcardForDelete = await _flashcardRepo.Get(request.FlashcardId);

            if (flashcardForDelete == null)
            {
                return new ActionReponseModel(false, $"Cannot find flashcard of id {request.FlashcardId}");
            }

            var getCollectionById = new GetCollectionByIdWithDailyStatsQ(flashcardForDelete.CollectionModelId, request.UserId);

            if (await _mediator.Send(getCollectionById) == null)
            {
                return new ActionReponseModel(false, "User do not own flashcard.");
            }

            await _flashcardRepo.Delete(flashcardForDelete);

            return new ActionReponseModel(true);
        }
    }
}
