using MediatR;
using MobileService.Entities.Enums;
using System;

namespace MobileService.Core.WorkUnits
{
    public class CalculatePracticeDateQ : IRequest<DateTime>
    {
        public CalculatePracticeDateQ(int correctAnsInRow, FlashcardProgress flashcardProgress)
        {
            CorrectAnsInRow = correctAnsInRow;
            FlashcardProgress = flashcardProgress;
        }

        public int CorrectAnsInRow { get; }
        public FlashcardProgress FlashcardProgress { get; }
    }
}
