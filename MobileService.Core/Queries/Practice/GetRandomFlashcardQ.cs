using MediatR;
using MobileService.Entities.DataTransferModels.Flashcard;

namespace MobileService.Core.Queries.Practice
{
    public class GetRandomFlashcardQ : IRequest<FlashcardPracticeGetModel>
    {
        public GetRandomFlashcardQ(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; }
    }
}
