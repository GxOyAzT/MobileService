using MediatR;
using MobileService.Entities.Models;
using System.Collections.Generic;

namespace MobileService.Core.Queries
{
    public class GetCollectionsListQ : IRequest<List<CollectionModel>>
    {
        public readonly string UserId;

        public GetCollectionsListQ(string userId)
        {
            UserId = userId;
        }
    }
}
