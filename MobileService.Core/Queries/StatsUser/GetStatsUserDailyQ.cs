using MediatR;
using MobileService.Entities.DataTransferModels.Statistics;

namespace MobileService.Core.Queries.StatsUser
{
    public class GetStatsUserDailyQ : IRequest<StatsUserDailyGetModel>
    {
        public GetStatsUserDailyQ(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; }
    }
}
