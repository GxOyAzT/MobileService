using MediatR;
using MobileService.Entities.DataTransferModels.Flashcard;

namespace MobileService.Core.Queries.Practice
{
    public class GetRandomExpiredFlashcardQ : IRequest<FlashcardPracticeGetModel>
    {
        public GetRandomExpiredFlashcardQ(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; }
    }
}
