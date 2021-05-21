using MediatR;
using MobileService.Entities.DataTransferModels.Statistics;

namespace MobileService.Core.Queries.StatsUser
{
    /// <summary>
    /// Calculate how many flashcards user has to learn each day next 7 days.
    /// </summary>
    public class GetStatsUserExpiredNextWeekQ : IRequest<StatsUserExpiredWeekGetModel>
    {
        public GetStatsUserExpiredNextWeekQ(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; }
    }
}
