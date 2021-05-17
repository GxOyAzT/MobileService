using Microsoft.EntityFrameworkCore;
using MobileService.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileService.DataAccess.Repos
{
    public class CollectionRepo : BaseRepo<CollectionModel>, ICollectionRepo
    {
        public CollectionRepo(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<List<CollectionModel>> GetAllByUserId(string userId)
        {
            return await _appDbContext.Collections
                .Where(e => e.UserId == userId)
                .Include(e => e.FlashcardModels).ThenInclude(e => e.FlashcardProgressModels)
                .ToListAsync();
        }

        public async Task<CollectionModel> GetByIdIncludeFlashcardProgress(Guid collectionId) =>
            await _appDbContext.Collections
                .Include(e => e.FlashcardModels).ThenInclude(e => e.FlashcardProgressModels)
                .FirstOrDefaultAsync(e => e.Id == collectionId);
    }
}
