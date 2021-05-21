using MediatR;
using MobileService.Core.Commands.StatsUser;
using MobileService.DataAccess.Repos;
using MobileService.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

#nullable enable

namespace MobileService.Core.Handlers.StatsUser
{
    class IncrementStatsUserH : IRequestHandler<IncrementStatsUserC, ActionReponseModel>
    {
        private readonly IStatsUserRepo _statsUserRepo;

        public IncrementStatsUserH(IStatsUserRepo statsUserRepo)
        {
            _statsUserRepo = statsUserRepo;
        }

        public async Task<ActionReponseModel> Handle(IncrementStatsUserC request, CancellationToken cancellationToken)
        {
            var statsFromToday = await _statsUserRepo.GetStatFromToday(request.UserId);

            if (statsFromToday == null)
            {
                await _statsUserRepo.Insert(new Entities.Models.StatsUserModel()
                {
                    Day = DateTime.Now.Date,
                    UserId = request.UserId,
                    FlashcardsTurnOverCount = 1
                });

                return new ActionReponseModel(true);
            }

            statsFromToday.FlashcardsTurnOverCount++;

            await _statsUserRepo.Update(statsFromToday);

            return new ActionReponseModel(true);
        }
    }
}
