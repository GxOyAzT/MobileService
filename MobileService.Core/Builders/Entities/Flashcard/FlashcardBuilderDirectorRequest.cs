using MediatR;
using MobileService.Entities.Models;
using System;

namespace MobileService.Core.Builders.Entities.Flashcard
{
    public class FlashcardBuilderDirectorRequest : IRequest<FlashcardModel>
    {
        public FlashcardBuilderDirectorRequest(string native, string foreign, Guid collectionId, IFlashcardBuilder flashcardBuilder)
        {
            Native = native;
            Foreign = foreign;
            CollectionId = collectionId;
            FlashcardBuilder = flashcardBuilder;
        }

        public string Native { get; }
        public string Foreign { get; }
        public Guid CollectionId { get; }
        public IFlashcardBuilder FlashcardBuilder { get; }
    }
}
