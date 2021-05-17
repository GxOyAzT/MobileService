using System;

namespace MobileService.Entities.DataTransferModels.Flashcard
{
    public class FlashcardInsertModel
    {
        public Guid CollectionId { get; set; }
        public string Foreign { get; set; }
        public string Native { get; set; }
    }
}
