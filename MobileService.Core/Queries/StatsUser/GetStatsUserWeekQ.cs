using MediatR;
using MobileService.Entities.DataTransferModels.Statistics;

namespace MobileService.Core.Queries.StatsUser
{
    public class GetStatsUserWeekQ : IRequest<StatsUserTurnOverWeekGetModel>
    {
        public GetStatsUserWeekQ(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; }
    }
}
