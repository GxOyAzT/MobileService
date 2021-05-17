using MobileService.Entities.Enums;
using System;

namespace MobileService.Entities.Models
{
    public class FlashcardProgressModel : BaseModel
    {
        public PracticeDirection PracticeDirection { get; set; }
        public DateTime PracticeDate { get; set; }
        public int CorrectInRow { get; set; }

        public Guid FlashcardModelId { get; set; }
        public FlashcardModel FlashcardModel { get; set; }
    }
}
