using MediatR;
using MobileService.Entities.DataTransferModels.Flashcard;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileService.Core.Queries.Flashcards
{
    public class GetFlashcardsListWithProgressesParallelQ : IRequest<List<FlashcardWithProgressionGetModel>>
    {
        /// <summary>
        /// Gets flashcards for list view with extra info about progresses.
        /// <para>Executes opearions in parallel.</para>
        /// </summary>
        public GetFlashcardsListWithProgressesParallelQ(Guid collectionId, string userId)
    {
        CollectionId = collectionId;
        UserId = userId;
    }

    public Guid CollectionId { get; }
    public string UserId { get; }
}
}
