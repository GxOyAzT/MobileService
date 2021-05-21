using MediatR;
using MobileService.Entities.Enums;
using System;

namespace MobileService.Core.WorkUnits
{
    /// <summary>
    /// Calculate next practice date based on
    /// correct ans in row and user actual knowledge.
    /// </summary>
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
