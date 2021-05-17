using MobileService.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileService.DataAccess.Repos
{
    public interface IFlashcardProgressRepo : IBaseRepo<FlashcardProgressModel>
    {
        Task<List<FlashcardProgressModel>> GetAllUserFlashcards(string userId);
    }
}
