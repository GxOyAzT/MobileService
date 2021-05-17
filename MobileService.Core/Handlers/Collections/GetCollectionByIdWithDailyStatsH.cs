using MediatR;
using MobileService.Core.Queries.Collections;
using MobileService.DataAccess.Repos;
using MobileService.Entities.DataTransferModels.Collection;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MobileService.Core.Handlers.Collections
{
    public class GetCollectionByIdWithDailyStatsH : IRequestHandler<GetCollectionByIdWithDailyStatsQ, CollectionWithDailyStatsGetModel>
    {
        private readonly ICollectionRepo _collectionRepo;

        public GetCollectionByIdWithDailyStatsH(
            ICollectionRepo collectionRepo)
        {
            _collectionRepo = collectionRepo;
        }

        public async Task<CollectionWithDailyStatsGetModel> Handle(GetCollectionByIdWithDailyStatsQ request, CancellationToken cancellationToken)
        {
            var collection = await _collectionRepo.GetByIdIncludeFlashcardProgress(request.CollectionId);

            if (collection == null)
            {
                return null;
            }

            if (collection.UserId != request.UserId)
            {
                return null;
            }

            var flashcardProgresses = collection.FlashcardModels.SelectMany(e => e.FlashcardProgressModels);

            return new CollectionWithDailyStatsGetModel()
            {
                Id = collection.Id,
                Name = collection.Name,
                NewFlashcards = flashcardProgresses.Where(e => e.PracticeDate == DateTime.MinValue).Count(),
                TotalFlashcards = flashcardProgresses.Count(),
                ToLearnFlashcards = flashcardProgresses.Where(e => e.PracticeDate <= DateTime.Now).Count()
            };
        }
    }
}
