using MediatR;
using MobileService.Entities;
using System;

namespace MobileService.Core.Commands.Flashcards
{
    public class DeleteFlashcardC : IRequest<ActionReponseModel>
    {
        public DeleteFlashcardC(Guid flashcardId, string userId)
        {
            FlashcardId = flashcardId;
            UserId = userId;
        }

        public Guid FlashcardId { get; }
        public string UserId { get; }
    }
}
