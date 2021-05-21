using System;

namespace MobileService.Entities.DataTransferModels.Flashcard
{
    public class FlashcardWithProgressionGetModel
    {
        public Guid Id { get; set; }
        public string Foreign { get; set; }
        public string Native { get; set; }

        public FlashcardWithProgressGetModel ProgressNativeToForeign { get; set; }
        public FlashcardWithProgressGetModel ProgressForeignToNative { get; set; }
    }

    public class FlashcardWithProgressGetModel
    {
        public int CorrectInRow { get; set; }
        public string PracticeDate { get; set; }
        public string PracticeDateIfCorrectAns { get; set; }
    }
}
