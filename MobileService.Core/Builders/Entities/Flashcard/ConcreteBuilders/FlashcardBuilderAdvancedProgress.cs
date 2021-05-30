using MobileService.Entities.Enums;
using MobileService.Entities.Models;
using System;
using System.Collections.Generic;

namespace MobileService.Core.Builders.Entities.Flashcard.ConcreteBuilders
{
    public class FlashcardBuilderAdvancedProgress : FlashcardBuilderParent, IFlashcardBuilder
    {
        public override void CreateProgresses()
        {
            FlashcardModel.FlashcardProgressModels = new List<FlashcardProgressModel>()
            {
                new FlashcardProgressModel()
                {
                    PracticeDirection = PracticeDirection.ForeignToNative,
                    PracticeDate = DateTime.Now.Date,
                    CorrectInRow = 8
                },
                new FlashcardProgressModel()
                {
                    PracticeDirection = PracticeDirection.NativeToForeign,
                    PracticeDate = DateTime.Now.Date,
                    CorrectInRow = 8
                }
            };
        }
    }
}
