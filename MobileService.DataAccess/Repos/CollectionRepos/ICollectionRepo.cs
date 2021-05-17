using MobileService.Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileService.DataAccess.Repos
{
    public interface ICollectionRepo : IBaseRepo<CollectionModel>
    {
        public Task<List<CollectionModel>> GetAllByUserId(string userId);
        public Task<CollectionModel> GetByIdIncludeFlashcardProgress(Guid collectionId);
    }
}
