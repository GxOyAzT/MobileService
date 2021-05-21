using MobileService.Entities.Enums;
using MobileService.Entities.Models;
using System;
using System.Collections.Generic;

namespace MobileService.Core.Builders
{
    public class FlashcardBuilder : IFlashcardBuilder
    {
        public FlashcardModel Build(string native, string foreign, Guid collectionId)
        {
            return new FlashcardModel()
            {
                Foreign = foreign,
                Native = native,
                CollectionModelId = collectionId,
                FlashcardProgressModels = new List<FlashcardProgressModel>()
                {
                    new FlashcardProgressModel()
                    {
                        PracticeDirection = PracticeDirection.ForeignToNative,
                        PracticeDate = DateTime.MinValue,
                        CorrectInRow = 0
                    },
                    new FlashcardProgressModel()
                    {
                        PracticeDirection = PracticeDirection.NativeToForeign,
                        PracticeDate = DateTime.MinValue,
                        CorrectInRow = 0
                    }
                }
            };
        }
    }
}
