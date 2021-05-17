using MobileService.Entities.Models;
using System;
using System.Threading.Tasks;

namespace MobileService.DataAccess.Repos
{
    public interface IStatsUserRepo : IBaseRepo<StatsUserModel> 
    {
        Task<StatsUserModel> GetStatFromToday(string userId);
        Task<StatsUserModel> GetStatFromDate(string userId, DateTime date);
    }
}
