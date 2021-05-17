using MediatR;
using MobileService.Entities.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MobileService.Core.WorkUnits
{
    public class CalculatePracticeDateH : IRequestHandler<CalculatePracticeDateQ, DateTime>
    {
        public Task<DateTime> Handle(CalculatePracticeDateQ request, CancellationToken cancellationToken)
        {
            if (request.FlashcardProgress == FlashcardProgress.UnDefined)
            {
                return Task.FromResult(DateTime.MinValue);
            }

            if (request.FlashcardProgress == FlashcardProgress.DontKnow)
            {
                return Task.FromResult(DateTime.Now.Date);
            }

            if (request.FlashcardProgress == FlashcardProgress.MediumKnow)
            {
                return Task.FromResult(DateTime.Now.Date.AddDays(1));
            }

            switch (request.CorrectAnsInRow)
            {
                case 0:
                    return Task.FromResult(DateTime.Now.Date.AddDays(1));
                case 1:
                    return Task.FromResult(DateTime.Now.Date.AddDays(2));
                case 2:
                    return Task.FromResult(DateTime.Now.Date.AddDays(3));
                case 3:
                    return Task.FromResult(DateTime.Now.Date.AddDays(7));
                case 4:
                    return Task.FromResult(DateTime.Now.Date.AddDays(14));
                case 5:
                    return Task.FromResult(DateTime.Now.Date.AddMonths(1));
                case 6:
                    return Task.FromResult(DateTime.Now.Date.AddMonths(3));
                case 7:
                    return Task.FromResult(DateTime.Now.Date.AddMonths(6));
                case 8:
                    return Task.FromResult(DateTime.Now.Date.AddYears(1));
                case 9:
                    return Task.FromResult(DateTime.Now.Date.AddYears(5));
                case 10:
                    return Task.FromResult(DateTime.MaxValue);
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
