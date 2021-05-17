using MobileService.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileService.Entities.DataTransferModels.Flashcard
{
    public class FlashcardPracticeGetModel
    {
        public Guid FlashcardProgressId { get; set; }
        public string Front { get; set; }
        public string Back { get; set; }
        public PracticeDirection PracticeDirection { get; set; }
    }
}
