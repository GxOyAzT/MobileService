using MediatR;
using MobileService.Core.Commands.Collections;
using MobileService.Core.Queries.Collections;
using MobileService.DataAccess.Repos;
using MobileService.Entities;
using MobileService.Entities.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MobileService.Core.Handlers.Collections
{
    public class UpdateCollectionH : IRequestHandler<UpdateCollectionC, ActionReponseModel>
    {
        private readonly ICollectionRepo _collectionRepo;
        private readonly IMediator _mediator;

        public UpdateCollectionH(
            ICollectionRepo collectionRepo,
            IMediator mediator)
        {
            _collectionRepo = collectionRepo;
            _mediator = mediator;
        }

        public async Task<ActionReponseModel> Handle(UpdateCollectionC request, CancellationToken cancellationToken)
        {
            if (request.Collection == null)
            {
                return new ActionReponseModel(false, "Incorrect input format");
            }

            var collection = (await _collectionRepo.GetAllByUserId(request.UserId)).FirstOrDefault(e => e.Id == request.Collection.Id);

            if (collection == null)
            {
                return new ActionReponseModel(false, "Cannot find collection");
            }

            collection.Name = request.Collection.Name;

            await _collectionRepo.Update(collection);

            return new ActionReponseModel(true);
        }
    }
}
