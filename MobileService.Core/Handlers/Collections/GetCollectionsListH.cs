using MediatR;
using MobileService.Core.Queries;
using MobileService.DataAccess.Repos;
using MobileService.Entities.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MobileService.Core.Handlers
{
    public class GetCollectionsListH : IRequestHandler<GetCollectionsListQ, List<CollectionModel>>
    {
        private readonly ICollectionRepo _collectionRepo;

        public GetCollectionsListH(ICollectionRepo collectionRepo)
        {
            _collectionRepo = collectionRepo;
        }

        public async Task<List<CollectionModel>> Handle(GetCollectionsListQ request, CancellationToken cancellationToken) =>
            await _collectionRepo.GetAllByUserId(request.UserId);
    }
}
