using MobileService.Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileService.DataAccess.Repos
{
    public interface IFlashcardRepo : IBaseRepo<FlashcardModel>
    {
        Task<List<FlashcardModel>> GetWhereCollectionId(Guid collectionId);
        Task<List<FlashcardModel>> GetWhereUserId(string userId);
    }
}
