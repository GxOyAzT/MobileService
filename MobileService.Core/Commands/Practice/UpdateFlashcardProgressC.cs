using MediatR;
using MobileService.Entities;
using MobileService.Entities.Enums;
using System;

namespace MobileService.Core.Commands.Practice
{
    public class UpdateFlashcardProgressC : IRequest<ActionReponseModel>
    {
        public UpdateFlashcardProgressC(Guid flashcardProgressId, FlashcardProgress flashcardProgress, string userId)
        {
            FlashcardProgressId = flashcardProgressId;
            FlashcardProgress = flashcardProgress;
            UserId = userId;
        }

        public Guid FlashcardProgressId { get; }
        public FlashcardProgress FlashcardProgress { get; }
        public string UserId { get; }
    }
}
