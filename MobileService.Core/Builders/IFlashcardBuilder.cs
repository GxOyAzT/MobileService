using MobileService.Entities.Models;
using System;

namespace MobileService.Core.Builders
{
    /// <summary>
    /// Builds new flashcard with two included progresses models:
    /// native to foreign and foreign to native.
    /// <para>This way created entity is ready to be added to databse.</para>
    /// </summary>
    public interface IFlashcardBuilder
    {
        FlashcardModel Build(string native, string foreign, Guid collectionId);
    }
}