using MediatR;
using MobileService.Core.Queries.Collections;
using MobileService.Core.Queries.Flashcards;
using MobileService.DataAccess.Repos;
using MobileService.Entities.DataTransferModels.Flashcard;
using MobileService.Entities.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MobileService.Core.Handlers.Flashcards
{
    public class GetFlashcardsListH : IRequestHandler<GetFlashcardsListQ, List<FlashcardModel>>
    {
        private readonly IFlashcardRepo _flashcardRepo;
        private readonly IMediator _mediator;

        public GetFlashcardsListH(
            IFlashcardRepo flashcardRepo,
            IMediator mediator)
        {
            _flashcardRepo = flashcardRepo;
            _mediator = mediator;
        }

        public async Task<List<FlashcardModel>> Handle(GetFlashcardsListQ request, CancellationToken cancellationToken)
        {
            var getCollectionById = new GetCollectionByIdWithDailyStatsQ(request.CollectionId, request.UserId);

            var collection = await _mediator.Send(getCollectionById);

            if (collection == null)
            {
                return new List<FlashcardModel>();
            }

            return await _flashcardRepo.GetWhereCollectionId(request.CollectionId);
        }
    }
}
