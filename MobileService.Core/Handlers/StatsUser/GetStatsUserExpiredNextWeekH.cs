using MediatR;
using MobileService.Core.Queries.StatsUser;
using MobileService.DataAccess.Repos;
using MobileService.Entities.DataTransferModels.Statistics;
using MobileService.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MobileService.Core.Handlers.StatsUser
{
    public class GetStatsUserExpiredNextWeekH : IRequestHandler<GetStatsUserExpiredNextWeekQ, StatsUserExpiredWeekGetModel>
    {
        private readonly IFlashcardProgressRepo _flashcardProgressRepo;

        public GetStatsUserExpiredNextWeekH(IFlashcardProgressRepo flashcardProgressRepo)
        {
            _flashcardProgressRepo = flashcardProgressRepo;
        }

        public async Task<StatsUserExpiredWeekGetModel> Handle(GetStatsUserExpiredNextWeekQ request, CancellationToken cancellationToken)
        {
            var flashcardProgreses = await _flashcardProgressRepo.GetAllUserFlashcards(request.UserId);

            return new StatsUserExpiredWeekGetModel()
            {
                TodayDate = DecorateDateDayMonth(DateTime.Now.Date),
                TodayCount = Count(flashcardProgreses, DateTime.Now.Date),
                TomorrowDate = DecorateDateDayMonth(DateTime.Now.Date.AddDays(1)),
                TomorrowCount = Count(flashcardProgreses, DateTime.Now.Date.AddDays(1)),
                ThreeDayDate = DecorateDateDayMonth(DateTime.Now.Date.AddDays(2)),
                ThreeDayCount = Count(flashcardProgreses, DateTime.Now.Date.AddDays(2)),
                FourDayDate = DecorateDateDayMonth(DateTime.Now.Date.AddDays(3)),
                FourDayCount = Count(flashcardProgreses, DateTime.Now.Date.AddDays(3)),
                FiveDayDate = DecorateDateDayMonth(DateTime.Now.Date.AddDays(4)),
                FiveDayCount = Count(flashcardProgreses, DateTime.Now.Date.AddDays(4)),
                SixDayDate = DecorateDateDayMonth(DateTime.Now.Date.AddDays(5)),
                SixDayCount = Count(flashcardProgreses, DateTime.Now.Date.AddDays(5)),
                SevenDayDate = DecorateDateDayMonth(DateTime.Now.Date.AddDays(6)),
                SevenDayCount = Count(flashcardProgreses, DateTime.Now.Date.AddDays(6))
            };
        }

        private int Count(List<FlashcardProgressModel> flashcards, DateTime dateTime)
        {
            return flashcards.Where(e => e.PracticeDate <= dateTime).Count();
        }

        private string DecorateDateDayMonth(DateTime date)
        {
            return date.DayOfWeek.ToString().Substring(0, 3);
        }
    }
}
