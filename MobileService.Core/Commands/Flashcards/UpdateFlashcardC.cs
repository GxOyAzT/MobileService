using MediatR;
using MobileService.Entities;
using MobileService.Entities.DataTransferModels.Flashcard;

namespace MobileService.Core.Commands.Flashcards
{
    public class UpdateFlashcardC : IRequest<ActionReponseModel>
    {
        public UpdateFlashcardC(FlashcardUpdateModel flashcard, string userId)
        {
            Flashcard = flashcard;
            UserId = userId;
        }

        public FlashcardUpdateModel Flashcard { get; }
        public string UserId { get; }
    }
}
