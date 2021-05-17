namespace MobileService.Entities.DataTransferModels.Statistics
{
    public class StatsUserProgressGetModel
    {
        public int NewFlashcards { get; set; }
        public int BaseKnowledge { get; set; }
        public int MediumKnowledge { get; set; }
        public int GoodKnowledge { get; set; }
        public int Remebered { get; set; }
    }
}
