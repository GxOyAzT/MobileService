using MediatR;
using MobileService.Entities.DataTransferModels.Flashcard;

namespace MobileService.Core.Queries.Practice
{
    public class GetRandomExpiredForChooseQ : IRequest<FlashcardPracticeChooseGetModel>
    {
        public GetRandomExpiredForChooseQ(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; }
    }
}
