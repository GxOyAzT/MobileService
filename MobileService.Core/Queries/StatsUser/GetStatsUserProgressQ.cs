using MediatR;
using MobileService.Entities.DataTransferModels.Statistics;

namespace MobileService.Core.Queries.StatsUser
{
    public class GetStatsUserProgressQ : IRequest<StatsUserProgressGetModel>
    {
        public GetStatsUserProgressQ(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; }
    }
}
