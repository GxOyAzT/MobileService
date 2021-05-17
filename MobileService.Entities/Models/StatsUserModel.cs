using System;

namespace MobileService.Entities.Models
{
    public class StatsUserModel : BaseModel
    {
        public string UserId { get; set; }
        public int FlashcardsTurnOverCount { get; set; }
        public DateTime Day { get; set; }
    }
}
