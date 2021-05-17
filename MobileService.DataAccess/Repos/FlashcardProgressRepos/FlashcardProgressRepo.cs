using Microsoft.EntityFrameworkCore;
using MobileService.Entities.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileService.DataAccess.Repos
{
    public class FlashcardProgressRepo : BaseRepo<FlashcardProgressModel>, IFlashcardProgressRepo
    {
        public FlashcardProgressRepo(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<List<FlashcardProgressModel>> GetAllUserFlashcards(string userId)
        {
            return await _appDbContext.Collections
                .Where(e => e.UserId == userId)
                .Include(e => e.FlashcardModels).ThenInclude(e => e.FlashcardProgressModels).ThenInclude(e => e.FlashcardModel)
                .SelectMany(e => e.FlashcardModels)
                .SelectMany(e => e.FlashcardProgressModels)
                .ToListAsync();
        }
    }
}
