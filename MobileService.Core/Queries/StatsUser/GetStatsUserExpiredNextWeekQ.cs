using MediatR;
using MobileService.Entities.DataTransferModels.Statistics;

namespace MobileService.Core.Queries.StatsUser
{
    public class GetStatsUserExpiredNextWeekQ : IRequest<StatsUserExpiredWeekGetModel>
    {
        public GetStatsUserExpiredNextWeekQ(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; }
    }
}
