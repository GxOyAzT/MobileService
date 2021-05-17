using MobileService.Entities.Enums;
using System;

namespace MobileService.Entities.DataTransferModels.Flashcard
{
    public class FlashcardPracticeMixedGetModel
    {
        public Guid FlashcardProgressId { get; set; }
        public string Front { get; set; }
        public string Back { get; set; }
        public PracticeDirection PracticeDirection { get; set; }
        public string AnsA { get; set; }
        public string AnsB { get; set; }
        public string AnsC { get; set; }
        public int CorrectAns { get; set; }
        public int CorrectAnsInRow { get; set; }
    }
}
