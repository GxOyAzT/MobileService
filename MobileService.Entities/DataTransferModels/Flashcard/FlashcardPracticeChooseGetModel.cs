using System;

namespace MobileService.Entities.DataTransferModels.Flashcard
{
    public class FlashcardPracticeChooseGetModel
    {
        public Guid FlashcardProgressId { get; set; }
        public string Front { get; set; }
        public string AnsA { get; set; }
        public string AnsB { get; set; }
        public string AnsC { get; set; }
        public int CorrectAns { get; set; }
    }
}
