using MediatR;
using MobileService.Core.Queries.StatsUser;
using MobileService.DataAccess.Repos;
using MobileService.Entities.DataTransferModels.Statistics;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

#nullable enable

namespace MobileService.Core.Handlers.StatsUser
{
    public class GetStatsUserProgressH : IRequestHandler<GetStatsUserProgressQ, StatsUserProgressGetModel>
    {
        private readonly IFlashcardProgressRepo _flashcardProgressRepo;

        public GetStatsUserProgressH(IFlashcardProgressRepo flashcardProgressRepo)
        {
            _flashcardProgressRepo = flashcardProgressRepo;
        }

        public async Task<StatsUserProgressGetModel> Handle(GetStatsUserProgressQ request, CancellationToken cancellationToken)
        {
            var flashcardProgreses = await _flashcardProgressRepo.GetAllUserFlashcards(request.UserId);

            return new StatsUserProgressGetModel()
            {
                NewFlashcards = flashcardProgreses.Where(e => e.PracticeDate == DateTime.MinValue).Count(),
                BaseKnowledge = flashcardProgreses.Where(e => (e.CorrectInRow >= 0 && e.CorrectInRow <= 3) && e.PracticeDate != DateTime.MinValue).Count(),
                MediumKnowledge = flashcardProgreses.Where(e => (e.CorrectInRow >= 4 && e.CorrectInRow <= 7)).Count(),
                GoodKnowledge = flashcardProgreses.Where(e => (e.CorrectInRow >= 8 && e.CorrectInRow <= 10)).Count(),
                Remebered = flashcardProgreses.Where(e => e.CorrectInRow > 10).Count()
            };
        }
    }
}
