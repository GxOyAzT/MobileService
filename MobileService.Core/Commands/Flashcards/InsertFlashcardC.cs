using MediatR;
using MobileService.Entities;
using MobileService.Entities.DataTransferModels.Flashcard;

namespace MobileService.Core.Commands.Flashcards
{
    public class InsertFlashcardC : IRequest<ActionReponseModel>
    {
        public InsertFlashcardC(FlashcardInsertModel flashcard, string userId)
        {
            Flashcard = flashcard;
            UserId = userId;
        }

        public FlashcardInsertModel Flashcard { get; }
        public string UserId { get; }
    }
}
