using MediatR;
using MobileService.Core.Builders;
using MobileService.Core.Commands.Flashcards;
using MobileService.Core.Queries.Collections;
using MobileService.DataAccess.Repos;
using MobileService.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MobileService.Core.Handlers.Flashcards
{
    public class InsertFlashcardH : IRequestHandler<InsertFlashcardC, ActionReponseModel>
    {
        private readonly IMediator _mediator;
        private readonly IFlashcardRepo _flashcardRepo;
        private readonly IFlashcardBuilder _flashcardBuilder;

        public InsertFlashcardH(
            IMediator mediator,
            IFlashcardRepo flashcardRepo,
            IFlashcardBuilder flashcardBuilder)
        {
            _mediator = mediator;
            _flashcardRepo = flashcardRepo;
            _flashcardBuilder = flashcardBuilder;
        }

        public async Task<ActionReponseModel> Handle(InsertFlashcardC request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Flashcard.Foreign) || string.IsNullOrEmpty(request.Flashcard.Native))
            {
                return new ActionReponseModel(false, "Foreign and native cannot be empty value.");
            }

            var getCollectionC = new GetCollectionByIdWithDailyStatsQ(request.Flashcard.CollectionId, request.UserId);

            if (await _mediator.Send(getCollectionC) == null)
            {
                return new ActionReponseModel(false, $"Cannot find collection of id {request.UserId}");
            }

            var inputModel = _flashcardBuilder.Build(request.Flashcard.Native, request.Flashcard.Foreign, request.Flashcard.CollectionId);

            await _flashcardRepo.Insert(inputModel);

            return new ActionReponseModel(true, string.Empty);
        }
    }
}
