using MediatR;
using MobileService.Entities;

namespace MobileService.Core.Commands.Collections
{
    public class InsertCollectionC : IRequest<ActionReponseModel>
    {
        public InsertCollectionC(Entities.DataTransferModels.Collection.CollectionInsertModel collection, string userId)
        {
            Collection = collection;
            UserId = userId;
        }

        public Entities.DataTransferModels.Collection.CollectionInsertModel Collection { get; }
        public string UserId { get; }
    }
}
