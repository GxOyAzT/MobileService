using MediatR;
using MobileService.Entities;

namespace MobileService.Core.Commands.StatsUser
{
    public class IncrementStatsUserC : IRequest<ActionReponseModel>
    {
        public IncrementStatsUserC(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; }
    }
}
