using MediatR;
using MobileService.Entities;
using MobileService.Entities.DataTransferModels.Collection;

namespace MobileService.Core.Commands.Collections
{
    public class UpdateCollectionC : IRequest<ActionReponseModel>
    {
        public UpdateCollectionC(CollectionUpdateModel collection, string userId)
        {
            Collection = collection;
            UserId = userId;
        }

        public CollectionUpdateModel Collection { get; }
        public string UserId { get; }
    }
}
