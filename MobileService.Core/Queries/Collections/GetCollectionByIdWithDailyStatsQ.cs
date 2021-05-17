using MediatR;
using MobileService.Entities.DataTransferModels.Collection;
using System;

namespace MobileService.Core.Queries.Collections
{
    public class GetCollectionByIdWithDailyStatsQ : IRequest<CollectionWithDailyStatsGetModel>
    {
        public GetCollectionByIdWithDailyStatsQ(Guid collectionId, string userId)
        {
            CollectionId = collectionId;
            UserId = userId;
        }

        public Guid CollectionId { get; }
        public string UserId { get; }
    }
}
