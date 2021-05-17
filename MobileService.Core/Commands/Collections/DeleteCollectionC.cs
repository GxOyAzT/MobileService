using MediatR;
using MobileService.Entities;
using System;

namespace MobileService.Core.Commands.Collections
{
    public class DeleteCollectionC : IRequest<ActionReponseModel>
    {
        public DeleteCollectionC(Guid collectionId, string userId)
        {
            CollectionId = collectionId;
            UserId = userId;
        }

        public Guid CollectionId { get; }
        public string UserId { get; }
    }
}
