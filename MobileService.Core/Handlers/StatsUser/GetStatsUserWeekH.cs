using MediatR;
using MobileService.Core.Queries.StatsUser;
using MobileService.DataAccess.Repos;
using MobileService.Entities.DataTransferModels.Statistics;
using System;
using System.Threading;
using System.Threading.Tasks;

#nullable enable

namespace MobileService.Core.Handlers.StatsUser
{
    public class GetStatsUserWeekH : IRequestHandler<GetStatsUserWeekQ, StatsUserTurnOverWeekGetModel>
    {
        private readonly IStatsUserRepo _statsUserRepo;

        public GetStatsUserWeekH(IStatsUserRepo statsUserRepo)
        {
            _statsUserRepo = statsUserRepo;
        }

        public async Task<StatsUserTurnOverWeekGetModel> Handle(GetStatsUserWeekQ request, CancellationToken cancellationToken)
        {
            return new StatsUserTurnOverWeekGetModel()
            {
                TodayDate = DecorateDateDayMonth(DateTime.Now.Date),
                TodayCount = await Count(request.UserId, DateTime.Now.Date),
                YesterdayDate = DecorateDateDayMonth(DateTime.Now.Date.AddDays(-1)),
                YesterdayCount = await Count(request.UserId, DateTime.Now.Date.AddDays(-1)),
                ThreeDayBeforeDate = DecorateDateDayMonth(DateTime.Now.Date.AddDays(-2)),
                ThreeDayBeforeCount = await Count(request.UserId, DateTime.Now.Date.AddDays(-2)),
                FourDayBeforeDate = DecorateDateDayMonth(DateTime.Now.Date.AddDays(-3)),
                FourDayBeforeCount = await Count(request.UserId, DateTime.Now.Date.AddDays(-3)),
                FiveDayBeforeDate = DecorateDateDayMonth(DateTime.Now.Date.AddDays(-4)),
                FiveDayBeforeCount = await Count(request.UserId, DateTime.Now.Date.AddDays(-4)),
                SixDayBeforeDate = DecorateDateDayMonth(DateTime.Now.Date.AddDays(-5)),
                SixDayBeforeCount = await Count(request.UserId, DateTime.Now.Date.AddDays(-5)),
                SevenDayBeforeDate = DecorateDateDayMonth(DateTime.Now.Date.AddDays(-6)),
                SevenDayBeforeCount = await Count(request.UserId, DateTime.Now.Date.AddDays(-6))
            };
        }

        private async Task<int> Count(string userId, DateTime date)
        {
            var stat = await _statsUserRepo.GetStatFromDate(userId, date);

            if (stat == null)
            {
                return 0;
            }

            return stat.FlashcardsTurnOverCount;
        }

        private string DecorateDateDayMonth(DateTime date)
        {
            return date.DayOfWeek.ToString().Substring(0,3);
        }
    }
}
