using MediatR;
using MobileService.Core.Commands.Collections;
using MobileService.DataAccess.Repos;
using MobileService.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MobileService.Core.Handlers.Collections
{
    public class DeleteCollectionH : IRequestHandler<DeleteCollectionC, ActionReponseModel>
    {
        private readonly ICollectionRepo _collectionRepo;

        public DeleteCollectionH(
            ICollectionRepo collectionRepo)
        {
            _collectionRepo = collectionRepo;
        }

        public async Task<ActionReponseModel> Handle(DeleteCollectionC request, CancellationToken cancellationToken)
        {
            var collection = (await _collectionRepo.GetAllByUserId(request.UserId)).FirstOrDefault(e => e.Id == request.CollectionId);

            if (collection == null)
            {
                return new ActionReponseModel(false, $"Cannot find collection of id {request.CollectionId}");
            }

            await _collectionRepo.Delete(collection);

            return new ActionReponseModel(true, string.Empty);
        }
    }
}
