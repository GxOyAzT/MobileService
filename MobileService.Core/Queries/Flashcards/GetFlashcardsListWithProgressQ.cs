using MediatR;
using MobileService.Entities.DataTransferModels.Flashcard;
using System;
using System.Collections.Generic;

namespace MobileService.Core.Queries.Flashcards
{
    public class GetFlashcardsListWithProgressQ : IRequest<List<FlashcardWithProgressionGetModel>>
    {
        /// <summary>
        /// Gets flashcards for list view with extra info about progresses.
        /// <para>Executes opearions synchronously.</para>
        /// </summary>
        public GetFlashcardsListWithProgressQ(Guid collectionId, string userId)
        {
            CollectionId = collectionId;
            UserId = userId;
        }

        public Guid CollectionId { get; }
        public string UserId { get; }
    }
}
