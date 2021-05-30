using MediatR;
using MobileService.Core.Builders.Entities.Flashcard.ConcreteBuilders;
using MobileService.Entities.Exceptions;
using MobileService.Entities.Models;
using System.Threading;
using System.Threading.Tasks;

namespace MobileService.Core.Builders.Entities.Flashcard
{
    public class FlashcardBuilderDirector : IRequestHandler<FlashcardBuilderDirectorRequest, FlashcardModel>
    {
        public Task<FlashcardModel> Handle(FlashcardBuilderDirectorRequest request, CancellationToken cancellationToken)
        {
            if (!(request.FlashcardBuilder is FlashcardBuilderBaseProgress ||
                request.FlashcardBuilder is FlashcardBuilderAdvancedProgress))
                throw new BuilderNotDefinedException("Director cannot handle passed director.");

            request.FlashcardBuilder.Reset();
            request.FlashcardBuilder.CreateBase(request.Native, request.Foreign, request.CollectionId);
            request.FlashcardBuilder.CreateProgresses();

            return Task.FromResult(request.FlashcardBuilder.GetModel());
        }
    }
}
