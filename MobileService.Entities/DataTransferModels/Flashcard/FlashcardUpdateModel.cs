using System;

namespace MobileService.Entities.DataTransferModels.Flashcard
{
    public class FlashcardUpdateModel
    {
        public Guid Id { get; set; }
        public Guid CollectionId { get; set; }
        public string Foreign { get; set; }
        public string Native { get; set; }
    }
}
