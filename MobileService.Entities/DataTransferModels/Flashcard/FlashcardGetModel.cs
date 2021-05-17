using System;

namespace MobileService.Entities.DataTransferModels.Flashcard
{
    public class FlashcardGetModel
    {
        public Guid Id { get; set; }
        public string Foreign { get; set; }
        public string Native { get; set; }
    }
}
