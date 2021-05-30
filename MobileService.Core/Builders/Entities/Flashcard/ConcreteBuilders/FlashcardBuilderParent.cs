using MobileService.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileService.Core.Builders.Entities.Flashcard.ConcreteBuilders
{
    public abstract class FlashcardBuilderParent : IFlashcardBuilder
    {
        protected FlashcardModel FlashcardModel { get; set; }

        public void CreateBase(string native, string foreign, Guid collectionId)
        {
            FlashcardModel = new FlashcardModel()
            {
                Native = native,
                Foreign = foreign,
                CollectionModelId = collectionId
            };
        }

        public abstract void CreateProgresses();

        public FlashcardModel GetModel() => FlashcardModel;

        public void Reset() => FlashcardModel = new FlashcardModel();
    }
}
