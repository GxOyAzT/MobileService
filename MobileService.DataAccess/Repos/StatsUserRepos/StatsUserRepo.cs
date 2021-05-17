using MobileService.Entities.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MobileService.DataAccess.Repos
{
    public class StatsUserRepo : BaseRepo<StatsUserModel>, IStatsUserRepo
    {
        public StatsUserRepo(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public Task<StatsUserModel> GetStatFromDate(string userId, DateTime date) =>
            Task.FromResult(_appDbContext.StatsUserModels
                .FirstOrDefault(e => e.UserId == userId && e.Day == date));

        public Task<StatsUserModel> GetStatFromToday(string userId) => 
            Task.FromResult(_appDbContext.StatsUserModels
                .FirstOrDefault(e => e.UserId == userId && e.Day == DateTime.Now.Date));
    }
}
