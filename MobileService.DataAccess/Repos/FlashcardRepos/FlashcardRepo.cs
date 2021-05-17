using Microsoft.EntityFrameworkCore;
using MobileService.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileService.DataAccess.Repos
{
    public class FlashcardRepo : BaseRepo<FlashcardModel>, IFlashcardRepo
    {
        public FlashcardRepo(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<List<FlashcardModel>> GetWhereCollectionId(Guid collectionId) =>
            await _appDbContext.Flashcards
                .Where(e => e.CollectionModelId == collectionId)
                .ToListAsync();

        public async Task<List<FlashcardModel>> GetWhereUserId(string userId) =>
            await _appDbContext.Collections
            .Include(e => e.FlashcardModels)
            .Where(e => e.UserId == userId)
            .SelectMany(e => e.FlashcardModels)
            .ToListAsync();
    }
}
