namespace MobileService.Entities.DataTransferModels.Statistics
{
    public class StatsUserExpiredWeekGetModel
    {
        public string TodayDate { get; set; }
        public int TodayCount { get; set; }

        public string TomorrowDate { get; set; }
        public int TomorrowCount { get; set; }

        public string ThreeDayDate { get; set; }
        public int ThreeDayCount { get; set; }

        public string FourDayDate { get; set; }
        public int FourDayCount { get; set; }

        public string FiveDayDate { get; set; }
        public int FiveDayCount { get; set; }

        public string SixDayDate { get; set; }
        public int SixDayCount { get; set; }

        public string SevenDayDate { get; set; }
        public int SevenDayCount { get; set; }
    }
}
