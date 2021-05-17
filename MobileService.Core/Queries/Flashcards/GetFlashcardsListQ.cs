using MediatR;
using MobileService.Entities.Models;
using System;
using System.Collections.Generic;

namespace MobileService.Core.Queries.Flashcards
{
    public class GetFlashcardsListQ : IRequest<List<FlashcardModel>>
    {
        public GetFlashcardsListQ(Guid collectionId, string userId)
        {
            CollectionId = collectionId;
            UserId = userId;
        }

        public Guid CollectionId { get; }
        public string UserId { get; }
    }
}
