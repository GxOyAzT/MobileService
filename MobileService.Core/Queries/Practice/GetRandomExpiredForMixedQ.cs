using MediatR;
using MobileService.Entities.DataTransferModels.Flashcard;

namespace MobileService.Core.Queries.Practice
{
    public class GetRandomExpiredForMixedQ : IRequest<FlashcardPracticeMixedGetModel>
    {
        public GetRandomExpiredForMixedQ(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; }
    }
}
