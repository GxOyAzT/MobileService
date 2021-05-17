using MediatR;
using MobileService.Core.Queries.StatsUser;
using MobileService.DataAccess.Repos;
using MobileService.Entities.DataTransferModels.Statistics;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MobileService.Core.Handlers.StatsUser
{
    public class GetStatsUserDailyH : IRequestHandler<GetStatsUserDailyQ, StatsUserDailyGetModel>
    {
        private readonly IFlashcardProgressRepo _flashcardProgressRepo;

        public GetStatsUserDailyH(IFlashcardProgressRepo flashcardProgressRepo)
        {
            _flashcardProgressRepo = flashcardProgressRepo;
        }

        public async Task<StatsUserDailyGetModel> Handle(GetStatsUserDailyQ request, CancellationToken cancellationToken)
        {
            var flashcardsProgresses = await _flashcardProgressRepo.GetAllUserFlashcards(request.UserId);

            return new StatsUserDailyGetModel()
            {
                TotalFlashcards = flashcardsProgresses.Count(),
                NewFlashcards = flashcardsProgresses.Where(e => e.PracticeDate == DateTime.MinValue).Count(),
                ToLearnFlashcards = flashcardsProgresses.Where(e => e.PracticeDate <= DateTime.Now).Count()
            };
        }
    }
}
