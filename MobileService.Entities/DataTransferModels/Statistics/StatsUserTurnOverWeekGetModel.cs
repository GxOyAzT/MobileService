namespace MobileService.Entities.DataTransferModels.Statistics
{
    public class StatsUserTurnOverWeekGetModel
    {
        public string TodayDate { get; set; }
        public int TodayCount { get; set; }

        public string YesterdayDate { get; set; }
        public int YesterdayCount { get; set; }

        public string ThreeDayBeforeDate { get; set; }
        public int ThreeDayBeforeCount { get; set; }

        public string FourDayBeforeDate { get; set; }
        public int FourDayBeforeCount { get; set; }

        public string FiveDayBeforeDate { get; set; }
        public int FiveDayBeforeCount { get; set; }

        public string SixDayBeforeDate { get; set; }
        public int SixDayBeforeCount { get; set; }

        public string SevenDayBeforeDate { get; set; }
        public int SevenDayBeforeCount { get; set; }
    }
}
