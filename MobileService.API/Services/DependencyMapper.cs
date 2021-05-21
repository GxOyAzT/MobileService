using Microsoft.Extensions.DependencyInjection;
using MobileService.Core.Builders;
using MobileService.DataAccess.Repos;
using MobileService.Entities.Models;

namespace MobileService.API.Services
{
    public static class DependencyMapper
    {
        public static void MapDependencies(this IServiceCollection services)
        {
            services.AddTransient<IGenerateToken, GenerateToken>();

            services.AddTransient<IFlashcardBuilder, FlashcardBuilder>();

            services.AddTransient<IBaseRepo<CollectionModel>, BaseRepo<CollectionModel>>();
            services.AddTransient<ICollectionRepo, CollectionRepo>();

            services.AddTransient<IBaseRepo<FlashcardModel>, BaseRepo<FlashcardModel>>();
            services.AddTransient<IFlashcardRepo, FlashcardRepo>();

            services.AddTransient<IBaseRepo<FlashcardProgressModel>, BaseRepo<FlashcardProgressModel>>();
            services.AddTransient<IFlashcardProgressRepo, FlashcardProgressRepo>();

            services.AddTransient<IBaseRepo<StatsUserModel>, BaseRepo<StatsUserModel>>();
            services.AddTransient<IStatsUserRepo, StatsUserRepo>();
        }
    }
}
