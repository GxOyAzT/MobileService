using MobileService.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileService.Core.Builders.Entities.Flashcard
{
    public interface IFlashcardBuilder
    {
        void CreateBase(string native, string foreign, Guid collectionId);
        void CreateProgresses();
        void Reset();
        FlashcardModel GetModel();
    }
}
