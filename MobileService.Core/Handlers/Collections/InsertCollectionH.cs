using AutoMapper;
using MediatR;
using MobileService.Core.Commands.Collections;
using MobileService.Core.Queries;
using MobileService.DataAccess.Repos;
using MobileService.Entities;
using MobileService.Entities.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MobileService.Core.Handlers.Collections
{
    public class InsertCollectionH : IRequestHandler<InsertCollectionC, ActionReponseModel>
    {
        private readonly ICollectionRepo _collectionRepo;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public InsertCollectionH(
            ICollectionRepo collectionRepo,
            IMediator mediator,
            IMapper mapper)
        {
            _collectionRepo = collectionRepo;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<ActionReponseModel> Handle(InsertCollectionC request, CancellationToken cancellationToken)
        {
            var getCollectionListQ = new GetCollectionsListQ(request.UserId);

            if (string.IsNullOrEmpty(request.Collection.Name))
            {
                return new ActionReponseModel(false, $"Collection name cannot be empty.");
            }

            if ((await _mediator.Send(getCollectionListQ)).Where(e => e.Name == request.Collection.Name).Any())
            {
                return new ActionReponseModel(false, $"Collection of name {request.Collection.Name}  already exists.");
            }

            var insertModel = _mapper.Map<CollectionModel>(request.Collection);

            insertModel.UserId = request.UserId;

            await _collectionRepo.Insert(insertModel);

            return new ActionReponseModel(true, string.Empty);
        }
    }
}
