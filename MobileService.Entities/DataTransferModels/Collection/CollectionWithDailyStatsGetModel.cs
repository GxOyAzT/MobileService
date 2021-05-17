using System;

namespace MobileService.Entities.DataTransferModels.Collection
{
    public class CollectionWithDailyStatsGetModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int TotalFlashcards { get; set; }
        public int NewFlashcards { get; set; }
        public int ToLearnFlashcards { get; set; }
    }
}
